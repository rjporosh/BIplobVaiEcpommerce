using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRUDOperation.Models
{
    public class Product
    {
        
        public long Id { get; set; }

        [Required(ErrorMessage = "Please Enter the Product Name!")]
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool IsActive { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public byte[] ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public long? StockId { get; set; }
        [ForeignKey("StockId")]
        public virtual Stock Stock { get; set; }
        public virtual List<ProductOrder> Orders { get; set; } //used for define relationship in dbcontext onmodel creating
        public long? VariantId { get; set; }
        public virtual Variant Variant { get; set; }

        public double InStock { get; set; }

        [MaxLength(250, ErrorMessage = "Maximum 250 Words")]
        public string ProductDescription { get; set; }

        public long ProductCode { get; set; }



    }
}
