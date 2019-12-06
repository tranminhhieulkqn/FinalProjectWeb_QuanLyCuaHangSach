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

        public string IDNhanVien { get; set; }
        [ForeignKey("IDNhanVien")]
        public virtual ApplicationUser NhanVien { get; set; }

        [ForeignKey("Sach")]
        public int IDSach { get; set; }
        public Sach Sach { get; set; }

        public DateTime NgayNhap { get; set; }
        public int SoLuong { get; set; }
        public string MoTa { get; set; }

    }
}
