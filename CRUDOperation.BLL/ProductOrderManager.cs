using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.BLL
{
    public class ProductOrderManager:Manager<ProductOrder>,IProductOrderManager
    {
        private IProductOrderRepository _productOrderRepository;

        public ProductOrderManager(IProductOrderRepository productOrderRepository):base(productOrderRepository)
        {
            _productOrderRepository = productOrderRepository;
        }


    }
}
