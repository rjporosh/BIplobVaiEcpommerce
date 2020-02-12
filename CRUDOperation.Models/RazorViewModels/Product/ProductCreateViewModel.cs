using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDOperation.Models.RazorViewModels.Product
{
    public class ProductCreateViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please Enter the Product Name!")]
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public virtual Models.Category Category { get; set; }
        public virtual List<Models.Category> CategoryList { get; set; }
        public string CategoryName { get; set; }
        public byte[] ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public long? StockId { get; set; }
        [ForeignKey("StockId")]
        public virtual Models.Stock Stock { get; set; }

        [NotMapped]
        public List<CRUDOperation.Models.Product> ProductList { get; set; }

        public long? VariantId { get; set; }
        public virtual Variant Variant { get; set; }


        public double InStock { get; set; }

        public string ProductDescription { get; set; }

        public long ProductCode { get; set; }
    }
}
