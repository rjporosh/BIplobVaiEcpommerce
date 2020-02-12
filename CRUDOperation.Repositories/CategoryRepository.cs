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
    public class CategoryRepository:EFRepository<Category>,ICategoryRepository
    {
        private CRUDOperationDbContext _db;
        public CategoryRepository(DbContext dbContext):base(dbContext)
        {
            _db = dbContext as CRUDOperationDbContext;
        }

        public ICollection<Category> GetAllSubCategory()
        {
            return _db.Categories.ToList();
        }

        public override ICollection<Category> GetAll()
        {
            return _db.Categories
                .Include(c => c.Products)
                .Include(c => c.Parent)
                .ToList();
        }

        //public override Category GetById(long id)
        //{
        //    var category = _db.Categories
        //        .Include(c => c.Parent)
        //        .Include(c => c.Products)
        //        .ToList();
        //    var aCategory = category
        //        .Where(c => c.Id == id)
        //        .FirstOrDefault();
        //    return aCategory;
        //}
    }
}