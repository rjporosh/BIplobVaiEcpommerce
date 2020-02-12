using CRUDOperation.Abstractions.BLL.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Abstractions.BLL
{
     public interface ICustomerManager:IManager<Customer>
    {
        bool CustomerExists(string entity);

    }
}
