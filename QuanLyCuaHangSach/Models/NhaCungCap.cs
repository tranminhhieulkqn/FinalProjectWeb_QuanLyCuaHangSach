using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class NhaCungCap
    {
        [Key]
        public int IDNhaCungCap { get; set; }
        public String TenNhaCungCap { get; set; }
        public String DiaChi { get; set; }
        public String SoDienThoai { get; set; }

        public ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}
