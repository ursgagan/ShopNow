using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = ShopNow.DAL.Entities.Image;

namespace ShopNow.DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ImageRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<Image> Create(Image image)
        {
            try
            {
                if (image != null)
                {
                    var addImage = _shopNowDbContext.Add<Image>(image);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addImage.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Image> GetAll()
        {
            try
            {
                var getImage = _shopNowDbContext.Image.Where(x => x.IsDeleted == false).ToList();
                if (getImage != null) return getImage;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Image GetById(Guid imageId)
        {
            try
            {
                if (imageId != null)
                {
                    var delImage = _shopNowDbContext.Image.FirstOrDefault(x => x.Id == imageId);
                    if (delImage != null) return delImage;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(Image image)
        {
            try
            {
                if (image != null)
                {
                    image.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(image);
                    _shopNowDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Image image)
        {
            try
            {
                if (image != null)
                {
                    var obj = _shopNowDbContext.Update(image);
                    if (obj != null) _shopNowDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Image>> AddMultipleImages(List<Image> images)
        {
            try
            {
                if (images != null && images.Any())
                {
                    await _shopNowDbContext.Image.AddRangeAsync(images);
                    await _shopNowDbContext.SaveChangesAsync();
                    return images;
                }
                else
                {
                    return null;
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                // Log or handle the exception appropriately
                throw new Exception("Error saving changes to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log or handle the exception appropriately
                throw new Exception("An error occurred while adding multiple images.", ex);
            }
        }
    }
}
