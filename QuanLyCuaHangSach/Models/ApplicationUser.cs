using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nhan Vien")]
        public string Name { get; set; }

        [NotMapped]
        public bool IsSuperUser { get; set; }
    }
}
