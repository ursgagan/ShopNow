using Microsoft.AspNetCore.Mvc;
using ShopNow.DAL.Entities;
using ShopNow.BAL.Services;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using ShopNow.Helpers;
using System.Collections.Generic;

namespace ShopNow.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductCategoryServices _productCategoryServices;
        private readonly ProductServices _productServices;
        private readonly ImageServices _imageServices;

        public AdminController(ProductCategoryServices productCategoryServices, ProductServices productServices, ImageServices imageServices)
        {
            _productCategoryServices = productCategoryServices;
            _productServices = productServices;
            _imageServices = imageServices;
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
            if (productCategory.Id != null && productCategory.Id != Guid.Empty)
            {
                _productCategoryServices.UpdateProductCategory(productCategory);
                TempData["SuccessMessage"] = "Product Category Updated Successfully";
            }
            else
            {
                await _productCategoryServices.AddProductCategory(productCategory);
                TempData["SuccessMessage"] = "Product Category Added Successfully";

            }
            ProductCategory productCategory1 = new ProductCategory();
            return View(productCategory1);
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
            var productData = await _productServices.AddProduct(product);

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
            if(imageList.Count > 0)
            {
              //  var imagesToAdd = new List<Image>(imageList);

                var addedImages = await _imageServices.AddMultipleImages(imageList);

            }

            //if (product.ProductId != null && product.ProductId != Guid.Empty)
            //{
            //   // _productServices.UpdateProduct(product);
            //    TempData["SuccessMessage"] = "Product Updated Successfully";
            //}
            //else
            //{
            //    //await _productServices.AddProduct(product);
            //    TempData["SuccessMessage"] = "Product Added Successfully";



            return View();
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



    }
}
