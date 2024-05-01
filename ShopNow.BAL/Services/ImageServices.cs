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
    public class ImageServices
    {
        
        private readonly IImageRepository _imageRepository;
        public ImageServices(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Image> AddImage(Image image)
        {
            try
            {
                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }
                else
                {
                    image.IsDeleted = false;
                    image.CreatedOn = DateTime.Now;
                    image.UpdatedOn = DateTime.Now;
                    return await _imageRepository.Create(image);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Image> GetAllImage()
        {
            try
            {
                return _imageRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteImage(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var image = _imageRepository.GetById(id);
                    _imageRepository.Delete(image);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Image GetImageById(Guid Id)
        {
            try
            {
                return _imageRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Image UpdateImage(Image image)
        {
            try
            {
                if (image.Id != null || image.Id != Guid.Empty)
                {
                    var obj = _imageRepository.GetAll().Where(x => x.Id == image.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.UpdatedOn = DateTime.Now;
                        _imageRepository.Update(obj);
                    }
                }
                return image;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Image>> AddMultipleImages(List<Image> image)
        {
            try
            {
                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }
                else
                {
                    foreach(var imageItem in image) {

                        imageItem.IsDeleted = false;
                        imageItem.CreatedOn = DateTime.Now;
                        imageItem.UpdatedOn = DateTime.Now;
                    }
                    return await _imageRepository.AddMultipleImages(image);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
