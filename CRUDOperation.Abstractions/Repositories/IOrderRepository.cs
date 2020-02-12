using CRUDOperation.Abstractions.Repositories.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Abstractions.Repositories
{
    public interface IOrderRepository: IRepository<Order>
    {
        bool OrderExists(long id);
        Order OrderPending();
    }
}
