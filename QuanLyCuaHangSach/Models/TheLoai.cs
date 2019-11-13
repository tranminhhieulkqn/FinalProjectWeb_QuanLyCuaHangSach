using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class TheLoai
    {
        [Key]
        public int IDTheLoai { get; set; }
        public String TenTheLoai { get; set; }

        public ICollection<Sach> Sachs { get; set; }
    }
}
