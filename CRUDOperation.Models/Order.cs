using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Models
{
    public class Order
    {
        public long Id { get; set; }
        [Unique]
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string Phone { get; set; }
        public string? Status { get; set; }
        public string? ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public virtual List<Item> ProductList { get; set; }
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<ProductOrder> Products { get; set; }
    }
}
