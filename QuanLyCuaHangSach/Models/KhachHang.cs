using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class KhachHang
    {
        [Key]
        public int IDKhachHang { get; set; }
        public String TenKhachHang { get; set; }
        public String EmailKhachHang { get; set; }
        public String SDTKhachHang { get; set; }

    }
}
