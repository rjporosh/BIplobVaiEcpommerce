using CRUDOperation.Abstractions.BLL.Base;
using CRUDOperation.Models;
using CRUDOperation.Models.APIViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Abstractions.BLL
{
    public interface IProductManager : IManager<Product>
    {
        ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria); //Just Generated GetByCriteria method

        Product Find(long id);

        ICollection<Product> GetByPrice(double price);
        ICollection<Product> GetByName(string Name);
        ICollection<Product> GetByCategory(string CategoryName);
    }
}
