using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public IEnumerable<ProductOrderModel> GetMyOrdersByCustomerId(Guid customerId)
        {
            try
            {
                if (customerId != Guid.Empty)
                {
                    var getMyOrdersDataByCustomerId = (from r in _shopNowDbContext.ProductOrder
                                                       where r.IsDeleted == false && r.CustomerId == customerId
                                                       join p in _shopNowDbContext.Product on r.ProductId equals p.Id into productJoin
                                                       from product in productJoin.DefaultIfEmpty()

                                                       select new ProductOrderModel
                                                       {
                                                           Id = r.Id,
                                                           CustomerId = r.CustomerId,
                                                           ProductId = r.ProductId,
                                                           Quantity = r.Quantity,
                                                           TotalPrice = r.TotalPrice,
                                                           IsDeleted = r.IsDeleted,
                                                           UpdatedBy = r.UpdatedBy,
                                                           CreatedBy = r.CreatedBy,
                                                           UpdatedOn = r.UpdatedOn,
                                                           CreatedOn = r.CreatedOn,

                                                           Product = product != null ? new Product
                                                           {
                                                               Id = product.Id,
                                                               Name = product.Name,
                                                               ProductCategoryId = product.ProductCategoryId,
                                                               Price = product.Price,
                                                               SupplierId = product.SupplierId,
                                                               IsDeleted = product.IsDeleted,
                                                               UpdatedBy = product.UpdatedBy,
                                                               CreatedBy = product.CreatedBy,
                                                               UpdatedOn = product.UpdatedOn,
                                                               CreatedOn = product.CreatedOn,
                                                               ProductDescription = product.ProductDescription,
                                                           } : null,

                                                           Customer = _shopNowDbContext.Customer.FirstOrDefault(c => c.Id == r.CustomerId)

                                                       });

                    return getMyOrdersDataByCustomerId.ToList();
                }
                else
                {
                    return new List<ProductOrderModel>();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            //try
            //{
            //    var getMyOrdersDataByCustomerId = _shopNowDbContext.ProductOrder.Include(a => a.Product).Where(x => x.CustomerId == customerId && x.IsDeleted == false);

            //    if (getMyOrdersDataByCustomerId != null)

            //        return getMyOrdersDataByCustomerId;

            //    else return null;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
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
                var getProductOrder = _shopNowDbContext.ProductOrder.Include(a => a.Product).Where(x => x.IsDeleted == false).ToList();

                if (getProductOrder != null)

                    return getProductOrder;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProductOrder GetById(Guid orderId)
        {
            try
            {
                if (orderId != null)
                {
                    var getOrderProductById = _shopNowDbContext.ProductOrder.Where(a => a.Id == orderId).FirstOrDefault();
                    if (getOrderProductById != null) return getOrderProductById;
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
        public bool UpdateProductOrderStatus(ProductOrder productOrder)
        {
            try
            {
                if (productOrder.Id != null)
                {
                    var productOrderData = _shopNowDbContext.Update(productOrder);
                    if (productOrderData != null) _shopNowDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

    }
}
