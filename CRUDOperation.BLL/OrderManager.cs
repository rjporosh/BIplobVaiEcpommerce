using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.BLL
{
    public class OrderManager:Manager<Order>,IOrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository):base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool OrderExists(long id)
        {
            return _orderRepository.OrderExists(id);
        }

        public Order OrderPending()
        {
            return _orderRepository.OrderPending();
        }
    }
}
