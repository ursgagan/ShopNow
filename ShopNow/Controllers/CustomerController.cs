using Microsoft.AspNetCore.Mvc;
using ShopNow.BAL.Services;
using ShopNow.DAL.Entities;
using System.Net.Mail;
using System.Net;

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

                    await SendLoginEmail(customer.EmailId, customer.FirstName);
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

        private async Task SendLoginEmail(string userEmail, string userName)
        {
            try
            {
               
                //string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                string folderPath = @"wwwroot\EmailTemplate\";

                // Specify the name of the HTML file
                string fileName = "WelcomeTemplate.html";

                string filePath = @"EmailTemplate";


                string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                emailBody = emailBody.Replace("[UserName]", userName);
               // emailBody = emailBody.Replace("[ForgotPasswordURL]", forgotPasswordURL);

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.UseDefaultCredentials = false;
                    // client.Credentials = new NetworkCredential("deepsinghh46@gmail.com", "@bc12345@");
                    client.Credentials = new NetworkCredential("deepsinghh46@gmail.com", "rstz jvja wqgd mvef");                
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

    }
}
