using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Models
{
    public class ProductOrder
    {
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public  Order Order { get; set; }
        public  Product Product { get; set; }

        public string? Status { get; set; }
        public long CustomerId { get; set; }
        public  Customer Customer { get; set; }
        public double? Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int DiscountPrice { get; set; }
    }
}
