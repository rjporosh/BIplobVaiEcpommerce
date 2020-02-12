using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUDOperation.Models
{
    public class Customer
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "Please Provide Your Name!")]
        public string Name { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimu 5")]
        public string Address { get; set; }

        [Required]
        [Range(0, 90)]
        public int LoyaltyPoint { get; set; }

        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public string Phone { get; set; }
    }
}
