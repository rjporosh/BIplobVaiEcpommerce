using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDOperation.Models
{
    public class Item
    {
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
