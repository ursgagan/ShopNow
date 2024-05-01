using Microsoft.AspNetCore.Mvc;
using ShopNow.BAL.Services;
using ShopNow.DAL.Entities;

namespace ShopNow.Helpers
{
    public class Common
    {     
        public static async Task<Image> SaveImage(IFormFile file)
        {

            var fileName = Path.GetFileName(file.FileName);
            Image image = new Image();
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var imageBytes = ms.ToArray();
                // Convert byte array to base64 string
                var base64String = Convert.ToBase64String(imageBytes);

                image.ImageName = fileName;
                image.ImageData = base64String;
                image.CreatedOn = DateTime.Now;
                image.UpdatedOn = DateTime.Now;
                image.IsDeleted = false;
           
            }
            return image;
        }
    }
}
