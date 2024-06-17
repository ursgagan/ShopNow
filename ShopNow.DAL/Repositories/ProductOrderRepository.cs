﻿using Microsoft.EntityFrameworkCore;
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
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ProductOrderRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<bool> Create(List<ProductOrder> productOrderList)
        {
            try
            {
                if (productOrderList != null)
                {
                    _shopNowDbContext.ProductOrder.AddRange(productOrderList);
                     await _shopNowDbContext.SaveChangesAsync();
                     return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductOrder> GetAll(Guid customerId)
        {
            try
            {
                var getMyOrdersDataByCustomerId = _shopNowDbContext.ProductOrder.Include(a => a.Product).Where(x => x.CustomerId == customerId && x.IsDeleted == false);

                if (getMyOrdersDataByCustomerId != null)

                    return getMyOrdersDataByCustomerId;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductOrder GetProductOrderByProductId(Guid productId)
        {
            try
            {
                if (productId != null)
                {
                    var productOrderByProductId = _shopNowDbContext.ProductOrder.Include(a => a.Product).ThenInclude(a => a.ProductImages).ThenInclude(a => a.Image)
                    .Where(a => a.ProductId == productId).FirstOrDefault();

                    if (productOrderByProductId != null) return productOrderByProductId;
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

        public IEnumerable<ProductOrder> GetAll()
        {
            try
            {
                var getProductOrder = _shopNowDbContext.ProductOrder.Where(x => x.IsDeleted == false).ToList();

                if (getProductOrder != null)

                    return getProductOrder;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
