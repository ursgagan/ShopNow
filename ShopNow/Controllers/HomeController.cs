using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopNow.BAL.Services;
using ShopNow.DAL.Entities;
using ShopNow.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ShopNow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductServices _productServices;
        private readonly ContactServices _contactServices;
        private readonly CustomerServices _customerServices;
        private readonly ShoppingCartServices _shoppingCartServices;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProductServices productServices, ContactServices contactServices, CustomerServices customerServices, ShoppingCartServices shoppingCartServices)
        {
            _logger = logger;
            _productServices = productServices;
            _contactServices = contactServices;
            _customerServices = customerServices;
            _shoppingCartServices = shoppingCartServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductDetails(string productId)
        {
            Product product = new Product();

            if (productId != null)
            {
                var productDetailId = new Guid(productId);
                product = _productServices.GetProductById(productDetailId);
            }
            return View(product);
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {
            try
            {
                if (contact.Id == null || contact.Id == Guid.Empty)
                {
                    await _contactServices.AddContact(contact);
                    TempData["SuccessMessage"] = "Contacts Added Successfully";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
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

                var getProductDataByCustomerId = _shoppingCartServices.GetAllProductByCustomerId(customerId).ToList();
                //bool isAdded = _shoppingCartServices.AddProductToShoppingCart(shoppingCart).Result;
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

    }
}
