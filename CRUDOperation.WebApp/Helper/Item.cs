using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDOperation.WebApp.Helper
{
    public class Item
    {
        public Product product { get; set; }

        //public string ProductCategoryName { get; set; }
        public int Quantity { get; set; }
    }
}
