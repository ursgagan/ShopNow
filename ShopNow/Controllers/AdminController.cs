using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShopNow.BAL.Services;
using ShopNow.DAL.Entities;
using ShopNow.Helpers;
using System.Security.Claims;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using ShopNow.DAL.Models;

namespace ShopNow.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductCategoryServices _productCategoryServices;
        private readonly ProductServices _productServices;
        private readonly ImageServices _imageServices;
        private readonly ProductImageServices _productImageServices;
        private readonly ProductOrderServices _productOrderServices;
        private readonly ReviewServices _reviewServices;
        private readonly ComplaintServices _complaintServices;
        private readonly CustomerServices _customerServices;
        private readonly AdminServices _adminServices;

        public AdminController(
            ProductCategoryServices productCategoryServices, 
            ProductServices productServices,
            ImageServices imageServices,
            ProductImageServices productImageServices,
            ProductOrderServices productOrderServices,
            ReviewServices reviewServices,
            ComplaintServices complaintServices,
            CustomerServices customerServices,
            AdminServices adminServices)
        {
            _productCategoryServices = productCategoryServices;
            _productServices = productServices;
            _imageServices = imageServices;
            _productImageServices = productImageServices;
            _productOrderServices = productOrderServices;
            _reviewServices = reviewServices;
            _complaintServices = complaintServices;
            _customerServices = customerServices;
            _adminServices = adminServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProductCategory(string productCategoryId)
        {
            ProductCategory productType = new ProductCategory();

            if (productCategoryId != null)
            {
                var pId = new Guid(productCategoryId);
                productType = _productCategoryServices.GetProductCategoryById(pId);
            }
            return View(productType);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductCategory(ProductCategory productCategory)
        {
            try
            {
                if (productCategory.Id != null && productCategory.Id != Guid.Empty)
                {
                    _productCategoryServices.UpdateProductCategory(productCategory);
                }
                else
                {
                    await _productCategoryServices.AddProductCategory(productCategory);
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ProductCategoryList()
        {
            return View();
        }

        public IActionResult GetProductCategoryList()
        { 
             var getProductCategoryList = _productCategoryServices.GetAllProductCategories().ToList();
              return Json(getProductCategoryList); 
        }
        public IActionResult DeleteProductCategory(Guid productCategoryId)
        {
            _productCategoryServices.DeleteProductCategory(productCategoryId);

            return RedirectToAction("ProductCategoryList", "Admin");
        }

        public IActionResult Product(string productId)
        {
            Product product = new Product();

            if (productId != null)
            {
                var pId = new Guid(productId);
                product = _productServices.GetProductById(pId); 

            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, List<IFormFile> imageFile)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (product.Id != null && product.Id != Guid.Empty)
                    {
                        _productServices.UpdateProduct(product);
                    }
                    else
                    {
                        product = await _productServices.AddProduct(product);
                    }

                    List<Image> addedImages = new List<Image>();
                    List<Image> imageList = new List<Image>();

                    if (imageFile.Count > 0)
                    {
                        foreach (var file in imageFile)
                        {
                            if (file != null && file.Length > 0)
                            {
                                Image imageData = await Common.SaveImage(file);
                                imageList.Add(imageData);
                            }
                        }
                    }

                    if (imageList.Count > 0)
                    {
                        addedImages = await _imageServices.AddMultipleImages(imageList);
                    }

                    if (addedImages.Count != 0)
                    {
                        List<ProductImages> productImagesList = new List<ProductImages>();

                        foreach (var addedProductImages in addedImages)
                        {
                            ProductImages productImages = new ProductImages();
                            productImages.ProductId = product.Id;
                            productImages.ImageId = addedProductImages.Id;
                            productImagesList.Add(productImages);
                        }
                        _productImageServices.AddMultipleProductImages(productImagesList);
                    }
                    scope.Complete();
                    return Json(true);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return Json(false);
                }
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ProductList()
        {
            return View();
        }

        public IActionResult GetProductList()
        {
            var adminIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

            if (adminIdClaim != null)
            {
                Guid adminId = new Guid(adminIdClaim.Value);

                var getProductList = _productServices.GetAllProduct().ToList();
                return Json(getProductList);
            }
            return Json(false);
        }

        public IActionResult GetProductListByPagination(int pageNumber)
        {
            var getProductList = _productServices.GetAllByPagination(pageNumber);

            return Json(getProductList);
        }

        public IActionResult DeleteProduct(Guid productId)
        {
            _productServices.DeleteProduct(productId);

            return RedirectToAction("ProductList", "Admin");
        }

        //public async Task<IActionResult> UploadImage(IFormFile imageFile)
        //{
        //    if (imageFile != null && imageFile.Length > 0)
        //    {
        //        var fileName = Path.GetFileName(imageFile.FileName);
        //        Image image = new Image();
        //        using (var ms = new MemoryStream())
        //        {
        //            await imageFile.CopyToAsync(ms);
        //            var imageBytes = ms.ToArray();
        //            // Convert byte array to base64 string
        //            var base64String = Convert.ToBase64String(imageBytes);

        //            image.ImageName = fileName;
        //            image.ImageData = base64String;
        //            image.CreatedOn = DateTime.Now;
        //            image.UpdatedOn = DateTime.Now;
        //            image.IsDeleted = false;
        //        }

        //            var product = _productServices.GetProductById();

        //            if (product.ImageId != null && (Guid)product.ImageId != Guid.Empty)
        //            {
        //                image.Id = (Guid)product.ImageId;
        //                var imageData = _productServices.UpdateImage(image);

        //                product.ImageId = image.Id;
        //                _productServices.UpdateProduct(product);
        //            }
        //            else
        //            {
        //                var imageData = await _productServices.AddImage(image);
        //                product.ImageId = imageData.Id;
        //                _productServices.UpdateProduct(product);
        //            }
        //            return Json(true);
        //            // You can save the base64 string or perform other operations here
        //        }
        //    }

        //public async Task<IActionResult> ÜpdateProduct(Product product, List<IFormFile> imageFile)
        //{
        //    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        try
        //        {
        //            if (product.Id != Guid.Empty)
        //            {
        //                _productServices.UpdateProduct(product);
        //            }

        //            List<Image> addedImages = new List<Image>();
        //            List<Image> imageList = new List<Image>();

        //            if (imageFile.Count > 0)
        //            {
        //                foreach (var file in imageFile)
        //                {
        //                    if (file != null && file.Length > 0)
        //                    {
        //                        Image imageData = await Common.SaveImage(file);
        //                        imageList.Add(imageData);
        //                    }
        //                }
        //            }

        //            if (imageList.Count > 0)
        //            {
        //                addedImages = await _imageServices.AddMultipleImages(imageList);
        //            }

        //            if (addedImages.Count != 0)
        //            {
        //                List<ProductImages> productImagesList = new List<ProductImages>();

        //                foreach (var addedProductImages in addedImages)
        //                {
        //                    ProductImages productImages = new ProductImages();
        //                    productImages.ProductId = product.Id;
        //                    productImages.ImageId = addedProductImages.Id;
        //                    productImagesList.Add(productImages);
        //                }

        //                _productImageServices.AddMultipleProductImages(productImagesList);
        //            }

        //            scope.Complete();
        //            return Json(true);

        //        }
        //        catch (Exception ex)
        //        {
        //            scope.Dispose();
        //            return Json(false);
        //        }
        //        return Json(true);
        //    }
        //}

        public IActionResult DeleteProductImage(Guid productImageId)
        {
            _productImageServices.DeleteProductImage(productImageId);
            return Json(true);
        }

        public IActionResult GetProductWithImageList()
        {
            var getProductWithImageList = _productImageServices.GetAllProductWithImage().ToList();

            return Json(getProductWithImageList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult OrderList()
        {
            return View();
        }
        public IActionResult getOrderList()
        {
            var getMyOrderList = _productOrderServices.GetAllMyOrders().ToList();

            return Json(getMyOrderList);
        }
        
        public async Task<IActionResult> UpdateProductOrderStatus(string orderId, string orderStatus)
        {
            var updateProductOrderStatus = _productOrderServices.updateProductOrderStatus(orderId, orderStatus);

            return Json(updateProductOrderStatus);
        }

        public IActionResult AllProductReviews()
        {
            return View();
        }
        public IActionResult getProductReviews()
        {
            var getProductReviewList = _reviewServices.GetAllProductReviews().ToList();

            foreach(var productReview in getProductReviewList)
            {
                productReview.Ratings = _productServices.GetRatingsByProductOrderId(productReview.ProductOrderId);
            }
            return Json(getProductReviewList);
        }
          
        [Authorize(Roles = "Admin")]
        public IActionResult AllProductComplaints()
        {
            return View();
        }

        public IActionResult getProductComplaints()
         {
            var getProductComplaintList = _complaintServices.GetAllProductComplaints().ToList();

            foreach(var productComplaint in getProductComplaintList)
            {
                  //productComplaint.ProductOrder.Customer = _customerServices.GetCustomerDataByProductComplaint(productComplaint.Id);
            }
            return Json(getProductComplaintList);
        }

        public async Task<IActionResult> UpdateComplaintStatus(string complaintId, string complaintStatus)
        {
            var updateComplaintStatus = _complaintServices.UpdateComplaintStatus(complaintId, complaintStatus);

            return Json(updateComplaintStatus);
        }

        public IActionResult Login()
        {
            return View();          
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(Customer customer)
        {
            {
                var user = _adminServices.isUserExist(customer.EmailId);

                if (user.UserRoles != null)
                {
                    if (PasswordHasher.VerifyPassword(customer.Password, user.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, (user.Id.ToString())),
                            new Claim(ClaimTypes.Email, user.EmailId),
                            new Claim(ClaimTypes.Role, "Admin"),
                        };

                        var userIdentity = new ClaimsIdentity("Custom");
                        userIdentity.AddClaims(claims);

                        var principal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), new AuthenticationProperties() { IsPersistent = true });
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            return Json(false);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AdminLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }

        private CustomerModel GetLoggedAdmin()
        {
            CustomerModel customerModel = new CustomerModel();
            Customer admin = new Customer();
            var adminClaims = ((ClaimsIdentity)User.Identity).Claims;

            // Find the claim with the email address
            var emailClaim = adminClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email");

            if (emailClaim != null)
            {
                var emailAddress = emailClaim.Value;

                admin = _customerServices.isUserExist(emailAddress);

                if (admin != null)
                {

                    customerModel.Id = admin.Id;
                    customerModel.FirstName = admin.FirstName;
                    customerModel.LastName = admin.LastName;
                    customerModel.EmailId = admin.EmailId; 
                    customerModel.IsDeleted = admin.IsDeleted;
                    customerModel.CreatedOn = admin.CreatedOn;
                    customerModel.UpdatedOn = admin.UpdatedOn;
                    customerModel.ResetCode = admin.ResetCode;

                    customerModel.Password = admin.Password;


                    return customerModel;
                }
            }
            return customerModel;
        }

        public IActionResult GetAdminData()
        {
            var getAdminData = GetLoggedAdmin();

            return Json(getAdminData);
        }

    }
}
