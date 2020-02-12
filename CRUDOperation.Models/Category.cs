using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRUDOperation.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; } //ParentId is nullable, because top-level categories have no parent.
        public virtual Category Parent { get; set; }

        [InverseProperty("Parent")]
        [JsonIgnore]
        public virtual List<Category> Childs { get; set; }

        [JsonIgnore] //used for Jsonloop Handling ignore
        public virtual List<Product> Products { get; set; }
    }
}
