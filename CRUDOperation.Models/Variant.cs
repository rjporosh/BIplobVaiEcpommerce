using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Models
{
    public class Variant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public long? SizeId { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
