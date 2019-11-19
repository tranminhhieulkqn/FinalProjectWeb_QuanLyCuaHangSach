using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class GiaoDich
    {
        [Key]
        public int IDGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }

        [NotMapped]
        public DateTime GioGiaoDich { get; set; }
        public string TenKhachHang { get; set; }
        public string SDTKhachHang { get; set; }
        public string EmailKhachHang { get; set; }
        public bool XacNhan { get; set; }

        public ICollection<ChiTietGiaoDich> ChiTietGiaoDichs { get; set; }
    }
}
