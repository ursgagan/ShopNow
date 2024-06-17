using Microsoft.AspNetCore.Mvc;
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
        private readonly ShoppingCartServices _shoppingCartServices;
        private readonly ProductServices _productServices;
        private readonly WishListServices _wishListServices;
        private readonly ProductOrderServices _productOrderServices;
        private readonly ReviewServices _reviewServices;
        public CustomerController(CustomerServices customerServices, AddressServices addressServices, ShoppingCartServices shoppingCartServices, ProductServices productServices, WishListServices wishListServices, ProductOrderServices productOrderServices, ReviewServices reviewServices)
        {
            _customerServices = customerServices;
            _addressServices = addressServices;
            _shoppingCartServices = shoppingCartServices;
            _productServices = productServices;
            _wishListServices = wishListServices;
            _productOrderServices = productOrderServices;
            _reviewServices = reviewServices;
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

                if (address.Id != null || address.Id == Guid.Empty)
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

        public IActionResult MyProfile()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                Guid customerId = new Guid(userId);

                var customer = _customerServices.GetCustomerById(customerId);

                return View(customer);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomerProfile(Customer customer)
        {
            try
            {
                if (customer.Id != null && customer.Id != Guid.Empty)
                {
                    _customerServices.UpdateCustomer(customer);
                    return Json(true);
                }

                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        public IActionResult GetShoppingCartByCustomerId()
        {
            var customerIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (customerIdClaim != null)
            {
                Guid customerId = new Guid(customerIdClaim.Value);
      
                var getProductDataByCustomerId = _shoppingCartServices.GetShoppingCartByCustomerId(customerId).ToList();
                return Json(getProductDataByCustomerId);
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToShoppingCart(ShoppingCart shoppingCart)
        {
            var productId = shoppingCart.ProductId;

            var getProduct = _productServices.GetProductById(productId);
            if (getProduct != null)
            {
                shoppingCart.TotalPrice = getProduct.Price * shoppingCart.Quantity;
            }

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                Guid customerId = new Guid(userId);

                shoppingCart.CustomerId = customerId;
                bool isAdded = _shoppingCartServices.AddProductToShoppingCart(shoppingCart).Result;
                return Json(isAdded);
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductToShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Id != null)
            {
                var getProduct = _productServices.GetProductById(shoppingCart.ProductId);
                if (getProduct != null)
                {
                    shoppingCart.TotalPrice = getProduct.Price * shoppingCart.Quantity;
                }
                bool isUpdated = _shoppingCartServices.UpdateShoppingCart(shoppingCart);
                return Json(isUpdated);
            }
            return Json(false);
        }

        public IActionResult DeleteProductFromShoppingCart(Guid shoppingCartId)
        {
            bool isDeleted = _shoppingCartServices.DeleteProductFromShoppingCart(shoppingCartId);

            return Json(isDeleted);
        }

        public IActionResult CheckOut()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                Guid customerId = new Guid(userId);

                var customer = _customerServices.GetCustomerById(customerId);

                ViewBag.getProductDataByCustomerId = _shoppingCartServices.GetShoppingCartByCustomerId(customerId).ToList();


                return View(customer);
            }
            return View();
        }
            
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(List<ProductOrder> productOrderList)
        {
            var customerIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (customerIdClaim != null)
            {
                Guid customerId = new Guid(customerIdClaim.Value);

                foreach (var productOrder in productOrderList)
                {
                    productOrder.CustomerId = customerId;
                    productOrder.IsDeleted = false;
                    productOrder.CreatedOn = DateTime.Now;
                    productOrder.UpdatedOn = DateTime.Now;
                }

                bool isAdded = _productOrderServices.AddProductOrder(productOrderList).Result;
                if (isAdded)
                {
                    _shoppingCartServices.DeleteShoppingCartByCustomerId(customerId);
                    return Json(isAdded);
                }
                return Json(false);
            }
            return Json(false);
        }
        public IActionResult GetAllCount()
        {
            var customerIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (customerIdClaim != null)
            {
                Guid customerId = new Guid(customerIdClaim.Value);

                var getProductCountByCustomerId = _shoppingCartServices.GetShoppingCartByCustomerId(customerId).Count();
                var getProductCountInWishListByCustomerId = _wishListServices.GetWishListByCustomerId(customerId).Count();
                var count = new
                {
                    shoppingCartCount = getProductCountByCustomerId,
                    wishListCount = getProductCountInWishListByCustomerId,
                };
                return Json(count);
            }
            return Json(false);
        }

        public IActionResult WishList()
        {
            return View();
        }

        public IActionResult GetWishListByCustomerId()
        {
            var customerIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (customerIdClaim != null)
            {
                Guid customerId = new Guid(customerIdClaim.Value);

                var getProductDataByCustomerId = _wishListServices.GetWishListByCustomerId(customerId).ToList();
                //bool isAdded = _shoppingCartServices.AddProductToShoppingCart(shoppingCart).Result;
                return Json(getProductDataByCustomerId);
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWishList(Wishlist wishlist)
        {
            var productId = wishlist.ProductId;

            var getProduct = _productServices.GetProductById(productId);
            if (getProduct != null)
            {
                wishlist.Price = getProduct.Price;
            }

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                Guid customerId = new Guid(userId);

                wishlist.CustomerId = customerId;
                bool isAdded = _wishListServices.AddProductToWishList(wishlist).Result;
                return Json(isAdded);
            }
            return Json(false);
        }

        public IActionResult DeleteProductFromWishlist(Guid wishListId)
        {
            bool isDeleted = _wishListServices.DeleteProductFromWishList(wishListId);

            return Json(isDeleted);
        }

        public IActionResult PlaceOrder()
        {
            return View();
        }

        public IActionResult MyOrders()
        {
            return View();
        }
        public IActionResult GetMyOrders()
        {
            var customerIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (customerIdClaim != null)
            {
                Guid customerId = new Guid(customerIdClaim.Value);

                var getProductDataByCustomerId = _productOrderServices.GetMyOrdersByCustomerId(customerId).ToList();
                return Json(getProductDataByCustomerId);
            }
            return Json(false);
        }

        public IActionResult RateAndReviewProduct(string productId)
        {
            ProductOrder productOrder = new ProductOrder();

            if (productId != null)
            {
                var productDetailId = new Guid(productId);
                productOrder = _productOrderServices.GetProductOrderByProductId(productDetailId);
            }
            return View(productOrder);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewModel reviewModel)
        {

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (userIdClaim != null)
            {
                bool isAdded = await _reviewServices.AddReview(reviewModel);

                return Json(isAdded);
            }
            return Json(false);
        }

    }
}
