using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRUDOperation.Models.RazorViewModels.Category
{
    public class CategoryCreateViewModel
    {
        public CategoryCreateViewModel()
        {
            Products = new List<Models.Product>(); //when getting 'Product is a namespace but used like a type'message then use Models
        }

        public string Name { get; set; }
        public long? ParentId { get; set; } //ParentId is nullable, because top-level categories have no parent.
        public virtual Models.Category Parent { get; set; }
        public string ParentCategoryName { get; set; }

        [InverseProperty("Parent")]
        [JsonIgnore]
        public virtual List<Models.Category> Childs { get; set; }

        [JsonIgnore] //used for Jsonloop Handling ignore
        public virtual List<Models.Product> Products { get; set; }

        public virtual List<Models.Category> Categories { get; set; }
    }
}
