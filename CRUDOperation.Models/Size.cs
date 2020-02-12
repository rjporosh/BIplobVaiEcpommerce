using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Models
{
    public class Size
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual List<Variant> Variants { get; set; }
    }
}
