using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.BLL
{
    public class CustomerManager:Manager<Customer>,ICustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository):base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool CustomerExists(string entity)
        {
            return _customerRepository.CustomerExists(entity);

            
        }
    }
}
