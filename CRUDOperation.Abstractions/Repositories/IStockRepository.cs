using CRUDOperation.Abstractions.Repositories.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Abstractions.Repositories
{
    public interface IStockRepository: IRepository<Stock>
    {
        Product GetByPId(long? Id);
        Stock check(long? Id);
    }
}
