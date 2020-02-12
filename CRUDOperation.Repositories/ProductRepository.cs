using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.DatabaseContext;
using CRUDOperation.Models;
using CRUDOperation.Models.APIViewModels;
using CRUDOperation.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUDOperation.Repositories
{
    public class ProductRepository:EFRepository<Product>,IProductRepository
    {
        private CRUDOperationDbContext _db;


        public ProductRepository(DbContext dbContext):base(dbContext)
        {
            _db = dbContext as CRUDOperationDbContext;
        }


        public override ICollection<Product> GetAll() //Just added category in product for show the category name in Product table.
        {
            return _db.Products
                      .Include(c => c.Category)
                      .ThenInclude(p => p.Parent)
                      .Include(c => c.Stock)
                      .ToList();
        }


        public List<Product> GetByCategory(int categoryId) /*Dropdown List Binding*/
        {
            return _db.Products
                      .Where(c => c.CategoryId == categoryId)
                      .ToList();
        }

        public ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria) //Just Implemented GetByCriteria method
        {
            var products = _db.Products.AsQueryable();
            if (criteria != null)
            {
                 //AsQueryable working in database && AsEnumerable working in memory after pulling data from database.
                //AsQueryable using for generate query without hit to database.
                // AsQueryable means deferred execution
                //Imediate execution using when call(ToList(),foreach,_db.products etc)
                if (!string.IsNullOrEmpty(criteria.Name)) //criteria.Name != null && criteria.Name !="" = !string.IsNullOrEmpty(criteria.Name)
                {
                    products = products.Where(p => p.Name.ToLower().Contains(criteria.Name.ToLower())); //ToLower using for case insencetive
                }

                if(criteria.FromPrice> 0)
                {
                    products = products.Where(p => p.Price >= criteria.FromPrice);
                }

                if(criteria.ToPrice> 0)
                {
                    products = products.Where(p => p.Price <= criteria.ToPrice);
                }
            }
            return products.ToList();//tolist using for imediate execution
        }

        public bool ProductExists(int? Id)
        {
            if (_db.Products.Any(id => id.Id == Id))
            {
                return true;
            }
            return false;
        }

        public Product Find(long id)
        {
            //return _db.Products.Find(id);
            return _db.Products.Include(c => c.Category).Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetByPrice(double price)
        {
            return _db.Products.Where(p => p.Price <= price).ToList();
        }  //Just Implemented GetByCriteria method

        public ICollection<Product> GetByName(string name)
        {
            return _db.Products.Where(p => p.Name.Contains(name)).ToList();
        }  //Just Implemented GetByCriteria method

        public ICollection<Product> GetByCategory(string categoryName)
        {
            return _db.Products.Where(p => p.Category.Name == categoryName).ToList();
        } //Just Implemented GetByCriteria method
    }
}



