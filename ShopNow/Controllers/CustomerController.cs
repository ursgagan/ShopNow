﻿using Microsoft.AspNetCore.Mvc;
using ShopNow.BAL.Services;
using ShopNow.DAL.Entities;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using ShopNow.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using ShopNow.DAL.Models;

namespace ShopNow.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerServices _customerServices;
        private readonly AddressServices _addressServices;
        public CustomerController(CustomerServices customerServices, AddressServices addressServices)
        {
            _customerServices = customerServices;
            _addressServices = addressServices;
        }
        public IActionResult SignUp(string customerId)
        {
            Customer customer = new Customer();
            if (customerId == null)
            {
                return View(customer);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(Customer customer, Address address)
        {
            try
            {
                await _addressServices.AddAddress(address);

                if(address.Id != null || address.Id == Guid.Empty)
                {
                    var generateOTP = GenerateOTP();
                    customer.ResetCode = generateOTP;
                    customer.AddressId = address.Id;
                    await _customerServices.AddCustomer(customer);

                    await SendLoginEmail(customer.EmailId, customer.FirstName, generateOTP);
                    return Json(true);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
                throw ex;
            }
           return Json(false);
        }

        private string GenerateOTP()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        private async Task SendLoginEmail(string userEmail, string userName, string generatedOTP)
        {
            try
            {
                var forgotPasswordURL = "https://localhost:44377/Customer/ForgotPassword?resetCode=" + generatedOTP;

                string folderPath = @"wwwroot\EmailTemplate\";

                // Specify the name of the HTML file
                string fileName = "WelcomeTemplate.html";

                string filePath = @"EmailTemplate";


                string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                emailBody = emailBody.Replace("[UserName]", userName);
                emailBody = emailBody.Replace("[ForgotPasswordURL]", forgotPasswordURL);

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.UseDefaultCredentials = false;
                    // client.Credentials = new NetworkCredential("deepsinghh46@gmail.com", "@bc12345@");
                    client.Credentials = new NetworkCredential("deepsinghh46@gmail.com", "iztv umyi eruq qbqc");                
                    client.EnableSsl = true;
                    client.Port = 587;

                    var message = new MailMessage
                    {
                        From = new MailAddress("deepsinghh46@gmail.com"),

                        Subject = "SignUp Successful",
                        Body = emailBody,
                        IsBodyHtml = true,
                    };

                    message.To.Add(userEmail);
                    message.To.Add("deepsinghh46@gmail.com");

                    await client.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or take appropriate action
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string emailId)
        {
            try
            {
                string folderPath = @"wwwroot\EmailTemplate\";

                // Specify the name of the HTML file
                string fileName = "ResetPassword.html";

                var existingCustomer = _customerServices.isUserExist(emailId);

                if (existingCustomer != null)
                {
                    var generatedOTP = GenerateOTP();
                    existingCustomer.ResetCode = generatedOTP;

                    _customerServices.UpdateCustomer(existingCustomer);

                   string firstName = existingCustomer.FirstName;
                   
                    var forgotPasswordURL = "https://localhost:44377/Customer/ForgotPassword?resetCode=" + generatedOTP;

                    string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                    emailBody = emailBody.Replace("[UserName]", firstName).Replace("[ForgotPasswordURL]", forgotPasswordURL);


                    using (var client = new SmtpClient("smtp.gmail.com"))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("deepsinghh46@gmail.com", "iztv umyi eruq qbqc");
                        client.EnableSsl = true;
                        client.Port = 587;

                        var message = new MailMessage
                        {
                            From = new MailAddress("deepsinghh46@gmail.com"),

                            Subject = "Forgotten Password",
                            Body = emailBody,
                            IsBodyHtml = true,
                        };

                        message.To.Add(emailId);
                       
                        await client.SendMailAsync(message);
                        return Json(true);
                    }
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }

        }

        [HttpGet]
        public IActionResult ForgotPassword(string resetCode)
        {
            ViewBag.ResetCode = resetCode;
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string password, string resetCodeId)
        {
            var getCustomerByResetCode = _customerServices.GetUserByResetCode(resetCodeId);
            if (getCustomerByResetCode != null)
            {
                getCustomerByResetCode.ResetCode = null;
                getCustomerByResetCode.Password = PasswordHasher.HashPassword(password);
                _customerServices.UpdateCustomer(getCustomerByResetCode);

                TempData["PasswordResetMessage"] = "Login Successfully";
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomerLogin(Customer customer)
        {
            {
                var user = _customerServices.isUserExist(customer.EmailId);

                if (user != null)
                {
                    if (PasswordHasher.VerifyPassword(customer.Password, user.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, (user.Id.ToString())),
                            new Claim(ClaimTypes.Email, user.EmailId),
                        };

                        var userIdentity = new ClaimsIdentity("Custom");
                        userIdentity.AddClaims(claims);

                        var principal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), new AuthenticationProperties() { IsPersistent = true });

                        TempData["SuccessMessage"] = "Login Successfully";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["PasswordIncorrectMessage"] = "Login Failed";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        private CustomerModel GetLoggedUser()
        {
           CustomerModel customerModel = new CustomerModel();
            Customer customer = new Customer();
            var userClaims = ((ClaimsIdentity)User.Identity).Claims;

            // Find the claim with the email address
            var emailClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email");

            if (emailClaim != null)
            {
                var emailAddress = emailClaim.Value;

                customer = _customerServices.isUserExist(emailAddress);

                if (customer != null)
                {

                    customerModel.Id = customer.Id;
                    customerModel.FirstName = customer.FirstName;
                    customerModel.LastName = customer.LastName;
                    customerModel.EmailId = customer.EmailId;
                    customerModel.Address.PhoneNumber = customer.Address.PhoneNumber;
                    customerModel.IsDeleted = customer.IsDeleted;
                    customerModel.CreatedOn = customer.CreatedOn;
                    customerModel.UpdatedOn = customer.UpdatedOn;
                    customerModel.ResetCode = customer.ResetCode;

                    customerModel.Password = customer.Password;


                    return customerModel;
                }
            }
            return customerModel;
        }

        public IActionResult GetCustomerData()
        {
            var getCustomerData = GetLoggedUser();

            return Json(getCustomerData);
        }


        [Authorize]
        public async Task<IActionResult> UserLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Customer");
        }
    }
}
