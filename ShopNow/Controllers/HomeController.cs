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
    }
}
