using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class PhieuNhap
    {
        [Key]
        public int IDPhieuNhap { get; set; }
        [ForeignKey("NhanVien")]
        public int IDNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }

        [ForeignKey("NhaCungCap")]
        public int IDNhaCungCap { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public DateTime NgayNhap { get; set; }

        public ICollection<CTPhieuNhap> CTPhieuNhaps { get; set; }

    }
}
