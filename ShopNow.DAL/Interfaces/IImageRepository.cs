﻿using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        public Task<List<Image>> AddMultipleImages(List<Image> images);
    }
}
