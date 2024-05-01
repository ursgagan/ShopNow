using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IImageRepository
    {
        public Task<Image> Create(Image image);
        public IEnumerable<Image> GetAll();
        public Image GetById(Guid Id);
        public void Update(Image image);
        public void Delete(Image image);
        public Task<List<Image>> AddMultipleImages(List<Image> images);
    }
}
