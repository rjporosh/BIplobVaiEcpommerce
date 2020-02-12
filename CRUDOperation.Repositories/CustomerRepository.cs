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
    public class CustomerRepository:EFRepository<Customer>,ICustomerRepository
    {

        private CRUDOperationDbContext _db;

        public CustomerRepository(DbContext dbContext):base(dbContext)
        {
            _db = dbContext as CRUDOperationDbContext;
        }


        public override ICollection<Customer> GetAll()
        {
            return _db.Customers.ToList();
        }
        public bool CustomerExists(string name)
        {
            if (_db.Customers.Any(c => c.Name == name))
            {
                return true;
            }
            return false;
        }
    }
}
