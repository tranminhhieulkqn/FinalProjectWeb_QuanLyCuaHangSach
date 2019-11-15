﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class PhieuXuat
    {
        [Key]
        public int IDPhieuXuat { get; set; }
        [ForeignKey("NhanVien")]
        public int IDNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }

        [ForeignKey("KhachHang")]
        public int IDKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }

        public DateTime NgayXuat { get; set; }
        
        [NotMapped]
        public DateTime GioXuat { get; set; }
        public bool DaXacNhan { get; set; }

        public ICollection<CTPhieuXuat> CTPhieuXuats { get; set; }
    }
}
