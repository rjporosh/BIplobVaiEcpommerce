using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.DatabaseContext;
using CRUDOperation.Models;
using CRUDOperation.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUDOperation.Repositories
{
    public class StockRepository: EFRepository<Stock>, IStockRepository
    {

        private CRUDOperationDbContext _db;

        public StockRepository(DbContext dbContext) : base(dbContext)
        {
            _db = dbContext as CRUDOperationDbContext;
        }


        public override ICollection<Stock> GetAll() //Just added product in stock for show the product name in stock table.
        {
            return _db.Stocks
                      .Include(c => c.Product)
                      .ToList();
        }
        public List<Stock> GetByProduct(int productId) /*Dropdown List Binding*/
        {
            return _db.Stocks
                .Where(s => s.ProductId == productId)
                .ToList();
        }

        public Product GetByPId(long? Id)
        {
            return _db.Products
                .Where(c => c.Id == Id)
                .Include(c => c.Stock)
                .FirstOrDefault();
        }
        public override bool Update(Stock entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        public override bool Delete(Stock entity)
        {

            _db.Stocks.Remove(entity);
            return _db.SaveChanges() > 0;
        }
        public Stock check(long? Id)
        {

            return _db.Stocks.Where(c => c.ProductId == Id).FirstOrDefault();

        }
    }
}
