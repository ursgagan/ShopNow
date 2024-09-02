using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.BAL.Services
{
    public class CImageService
    {
        private readonly ICategoryImageRepository _categoryImageRepository;
        public CImageService(ICategoryImageRepository categoryImageRepository)
        {
            _categoryImageRepository = categoryImageRepository;
        }

        public async Task<CImage> AddImage(CImage categoryImage)
        {
            try
            {
                if (categoryImage == null)
                {
                    throw new ArgumentNullException(nameof(categoryImage));
                }
                else
                {
                    categoryImage.IsDeleted = false;
                    categoryImage.CreatedOn = DateTime.Now;
                    categoryImage.UpdatedOn = DateTime.Now;
                    return await _categoryImageRepository.Create(categoryImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }  

        public IEnumerable<CImage> GetAllCategoryImage()
        {
            try
            {
                return _categoryImageRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCategoryImage(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var image = _categoryImageRepository.GetById(id);
                    _categoryImageRepository.Delete(image);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CImage GetCategoryImageById(Guid Id)
        {
            try
            {
                return _categoryImageRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CImage UpdateCategoryImage(CImage categoryImage)
        {
            try
            {
                if (categoryImage.Id != null || categoryImage.Id != Guid.Empty)
                {
                    var obj = _categoryImageRepository.GetAll().Where(x => x.Id == categoryImage.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.UpdatedOn = DateTime.Now;
                        _categoryImageRepository.Update(obj);
                    }
                }
                return categoryImage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<CImage>> AddMultipleImages(List<CImage> categoryImage)
        {
            try
            {
                if (categoryImage == null)
                {
                    throw new ArgumentNullException(nameof(categoryImage));
                }
                else
                {
                    foreach (var imageItem in categoryImage)
                    {

                        imageItem.IsDeleted = false;
                        imageItem.CreatedOn = DateTime.Now;
                        imageItem.UpdatedOn = DateTime.Now;
                    }
                    return await _categoryImageRepository.AddMultipleImages(categoryImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
