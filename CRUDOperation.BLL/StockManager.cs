using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.BLL
{
    public class StockManager:Manager<Stock>,IStockManager
    {
        private IStockRepository _stockRepository;

        public StockManager(IStockRepository stockRepository):base(stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Product GetByPId(long? Id)
        {
            return _stockRepository.GetByPId(Id);
        }
        public Stock check(long? Id)
        {
            return _stockRepository.check(Id);
        }
    }
}
