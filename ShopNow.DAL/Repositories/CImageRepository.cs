using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class CImageRepository : ICategoryImageRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public CImageRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<CImage> Create(CImage categoryImage)
        {
            try
            {
                if (categoryImage != null)
                {
                    var addImage = _shopNowDbContext.Add<CImage>(categoryImage);
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
        public IEnumerable<CImage> GetAll()
        {
            try
            {
                var getImage = _shopNowDbContext.CImage.Where(x => x.IsDeleted == false).ToList();
                if (getImage != null) return getImage;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CImage GetById(Guid categoryImageId)
        {
            try
            {
                if (categoryImageId != null)
                {
                    var delCategoryImage = _shopNowDbContext.CImage.FirstOrDefault(x => x.Id == categoryImageId);
                    if (delCategoryImage != null) return delCategoryImage;
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

        public void Delete(CImage categoryImage)
        {
            try
            {
                if (categoryImage != null)
                {
                    categoryImage.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(categoryImage);
                    _shopNowDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(CImage categoryImage)
        {
            try
            {
                if (categoryImage != null)
                {
                    var obj = _shopNowDbContext.Update(categoryImage);
                    if (obj != null) _shopNowDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CImage>> AddMultipleImages(List<CImage> categoryImages)
        {
            try
            {
                if (categoryImages != null && categoryImages.Any())
                {
                    await _shopNowDbContext.CImage.AddRangeAsync(categoryImages);
                    await _shopNowDbContext.SaveChangesAsync();
                    return categoryImages;
                }
                else
                {
                    return null;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding multiple images.", ex);
            }
        }

    }
}
