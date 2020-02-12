using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.DatabaseContext;
using CRUDOperation.Models;
using CRUDOperation.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CRUDOperation.Repositories
{
    public class ProductOrderRepository: EFRepository<ProductOrder>, IProductOrderRepository
    {
        private CRUDOperationDbContext _db;

        public ProductOrderRepository(CRUDOperationDbContext dbContext):base(dbContext)
        {
            _db = dbContext as CRUDOperationDbContext;
        }

        public override ICollection<ProductOrder> GetAll()
        {
            return _db.ProductOrder
                .Include(c => c.Product)
                .ThenInclude(c=>c.Category)
                .Include(c => c.Order)
                .ThenInclude(c=>c.Customer)
                .ToList();
        }
    }
}
