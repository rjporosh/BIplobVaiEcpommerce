using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRUDOperation.Models
{
    public class Stock
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        public string Unit  { get; set; }


    }
}
