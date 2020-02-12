using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRUDOperation.Models.RazorViewModels.Stock
{
    public class StockCreateViewModel
    {
        public long ProductId { get; set; }

        public int Quantity { get; set; }
        public string Unit { get; set; }


        [NotMapped]
        public List<CRUDOperation.Models.Stock> StockList { get; set; }
    }
}
