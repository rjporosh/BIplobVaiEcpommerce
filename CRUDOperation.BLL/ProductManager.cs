using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL.Base;
using CRUDOperation.Models;
using CRUDOperation.Models.APIViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUDOperation.BLL
{
    public class ProductManager : Manager<Product>, IProductManager
    {
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }


        public override Product GetById(long id)
        {
            var product = _productRepository.GetById(id);
            if (product.IsActive)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
        public ICollection<Product> GetByCriteria(ProductSearchCriteriaVM model) //Just Implemented GetByCriteria method
        {
            return _productRepository.GetByCriteria(model);
        }

        public override bool Delete(Product entity)
        {
            entity.IsActive = false;
            return _productRepository.Update(entity);
        }


        public Product Find(long Id)
        {
            return _productRepository.Find(Id);
        }

        public ICollection<Product> GetByPrice(double price)
        {
            return _productRepository.GetByPrice(price);
        }

        public ICollection<Product> GetByName(string Name)
        {
            return _productRepository.GetByName(Name);
        }

        public ICollection<Product> GetByCategory(string CategoryName)
        {
            return _productRepository.GetByCategory(CategoryName);
        }
    }
}
