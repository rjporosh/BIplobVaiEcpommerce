using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUDOperation.Models.RazorViewModels.Order
{
    public class OrderCreateViewModel
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        [Required(ErrorMessage = "Please provide Shipment Address!")]
        public string ShippingAddress { get; set; }
        [Required(ErrorMessage = "Please Select Payment Method!")]
        public string PaymentMethod { get; set; }
        [ServiceStack.DataAnnotations.Unique]
        [Required(ErrorMessage = "Please provide OrderNo!")]
        public string OrderNo { get; set; }
        public string Phone { get; set; }
        // public Decimal? DiscountPercentage { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<Models.ProductOrder> Products { get; set; }
        public List<Models.Order> OrderList { get; set; }
        public List<Item> ProductList { get; set; }
    }
}
