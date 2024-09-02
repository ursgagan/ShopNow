using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface ICategoryImageRepository
    {
        public Task<CImage> Create(CImage categoryImage);
        public IEnumerable<CImage> GetAll();
        public void Delete(Guid id);
        public CImage GetById(Guid Id);
        public CImage Update(CImage categoryImage);
        public Task<List<CImage>> AddMultipleImages(List<CImage> categoryImage)




    }
}
