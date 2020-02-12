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
    public class OrderRepository: EFRepository<Order>,IOrderRepository
    {
        private CRUDOperationDbContext _db;

        public OrderRepository( CRUDOperationDbContext dbContext):base(dbContext)
        {
            _db = dbContext as CRUDOperationDbContext;
        }

        public override ICollection<Order> GetAll()
        {
            return _db.Orders
                .Include(c => c.Products)
                .ToList();
        }

        public override Order GetById(long id)
        {
            return _db.Orders
                .Include(c => c.Customer)
                .Include(c => c.Products)

                .Where(c => c.Id == id)
                .FirstOrDefault();
        }
        public bool OrderExists(long Id)
        {
            return _db.Orders.Any(c => c.Id == Id);
        }

        public Order OrderPending()
        {
            return _db.Orders.Where(c => c.Status == "Pending").FirstOrDefault();
        }
    }
}
