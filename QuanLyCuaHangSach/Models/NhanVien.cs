using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class NhanVien
    {
        [Key]
        public int IDNhanVien { get; set; }
        public String HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public String SoDienThoai { get; set; }
        public String Anh { get; set; }

        public ICollection<PhieuNhap> PhieuNhaps { get; set; }
        public ICollection<PhieuXuat> PhieuXuats { get; set; }
    }
}
