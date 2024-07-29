using Microsoft.AspNetCore.Mvc;
using ShopNow.BAL.Services;
using ShopNow.DAL.Entities;
using ShopNow.Helpers;
using System.Transactions;

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

        public AdminController(ProductCategoryServices productCategoryServices, ProductServices productServices, ImageServices imageServices, ProductImageServices productImageServices, ProductOrderServices productOrderServices, ReviewServices reviewServices)
        {
            _productCategoryServices = productCategoryServices;
            _productServices = productServices;
            _imageServices = imageServices;
            _productImageServices = productImageServices;
            _productOrderServices = productOrderServices;
            _reviewServices = reviewServices;
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
        public IActionResult ProductList()
        {
            return View();
        }

        public IActionResult GetProductList()
        {
            var getProductList = _productServices.GetAllProduct().ToList();
            return Json(getProductList);
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
                productReview.Ratings = _productServices.GetRatingsByProductId(productReview.Id);
            }
            return Json(getProductReviewList);
        }
    }
}
