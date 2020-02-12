﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Models.RazorViewModels.ProductOrder
{
    public class ProductOrderVM
    {
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public Decimal? Quantity { get; set; }
        public string Status { get; set; }
        public int? DiscountPercentage { get; set; }
        public Decimal? UnitPrice { get; set; }
        public Models.Customer Customer { get; set; }
        public long CustomerId { get; set; }
        public Models.Order Order { get; set; }
        public Models.Product Product { get; set; }
    }
}
