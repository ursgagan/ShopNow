using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopNow.BAL.Services;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Models;
using ShopNow.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ShopNow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductServices _productServices;
        private readonly ContactServices _contactServices;
        private readonly CustomerServices _customerServices;
        private readonly ShoppingCartServices _shoppingCartServices;
        private readonly ILogger<HomeController> _logger;
        private readonly ProductImageServices _productImageServices;
        private readonly ProductCategoryServices _productCategoryServices;
        private readonly ProductOrderServices _productOrderServices;
        private readonly ComplaintServices _complaintServices;

        public HomeController(ILogger<HomeController> logger, ProductServices productServices, ContactServices contactServices, CustomerServices customerServices, ShoppingCartServices shoppingCartServices, ProductImageServices productImageServices, ProductCategoryServices productCategoryServices, ProductOrderServices productOrderServices, ComplaintServices complaintServices)
        {
            _logger = logger;
            _productServices = productServices;
            _contactServices = contactServices;
            _customerServices = customerServices;
            _shoppingCartServices = shoppingCartServices;
            _productImageServices = productImageServices;
            _productCategoryServices = productCategoryServices;
            _productOrderServices = productOrderServices;
            _complaintServices = complaintServices;
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
        public IActionResult Shop(Guid productCategoryId)
        {
            ViewBag.ProductCategoryId = productCategoryId;
            return View();
        }

        public IActionResult GetProductByFilters(FilterProductModel filterProductModel)
        {

            var getProductWithImageList = _productImageServices.GetProductByFilters(filterProductModel).ToList();

            foreach(var product in getProductWithImageList)
            {
                product.Ratings = _productServices.GetRatingsByProductId(product.Id);
            }

            if(filterProductModel.Rating != 0 && filterProductModel.Rating != null)
            {
                getProductWithImageList = getProductWithImageList.Where(p => p.Ratings.Count != 0 &&
                ((p.Ratings.Select(a => a.Rate).Sum() / p.Ratings.Count()) == filterProductModel.Rating)).ToList();

            }
            
            return Json(getProductWithImageList);
        }

        public IActionResult GetRatingsByProductId(string productId)
        {
            List<RatingModel> rating = new List<RatingModel>();

            if (productId != null)
            {
                var productDetailId = new Guid(productId);
                rating = _productServices.GetRatingsByProductId(productDetailId);
            }
            return Json(rating);
        }
        public IActionResult AddComplaint(string productId)
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
        public async Task<IActionResult> AddComplaint(Complaint productComplaint)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (userIdClaim != null)
            {
                var isAdded = await _complaintServices.AddComplaint(productComplaint);

                return Json(isAdded);
            }
            return 0.(false);
        }
    }
}

