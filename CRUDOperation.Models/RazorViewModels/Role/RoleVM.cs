using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUDOperation.Models.RazorViewModels.Role
{
    public class RoleVM
    {
        [Required]
        public string RoleName { get; set; }
    }
}
