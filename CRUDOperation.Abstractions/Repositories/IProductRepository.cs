using CRUDOperation.Abstractions.Repositories.Base;
using CRUDOperation.Models;
using CRUDOperation.Models.APIViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Abstractions.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria);

        Product Find(long id);
        ICollection<Product> GetByPrice(double price);
        ICollection<Product> GetByName(string name);
        ICollection<Product> GetByCategory(string categoryName);

        //ICollection<Product> GetById(long id);
    }
}
