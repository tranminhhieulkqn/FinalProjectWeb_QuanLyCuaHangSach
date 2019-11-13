using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class CTPhieuXuat
    {
        [Key]
        public int IDCTPhieuXuat { get; set; }
        [ForeignKey("PhieuXuat")]
        public int IDPhieuXuat { get; set; }
        public PhieuXuat PhieuXuat { get; set; }

        [ForeignKey("Sach")]
        public int IDSach { get; set; }
        public Sach Sach { get; set; }

        public int SoLuong { get; set; }
        public int DonGia { get; set; }

    }
}
