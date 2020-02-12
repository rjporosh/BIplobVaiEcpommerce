﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? ShippingAddress { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; }
    }
}
