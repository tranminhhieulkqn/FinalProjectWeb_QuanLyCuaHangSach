using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class Sach
    {
        [Key]
        public int IDSach { get; set; }
        public String TenSach { get; set; }

        [ForeignKey("TacGia")]
        public int IDTacGia { get; set; }
        public TacGia TacGia { get; set; }

        [ForeignKey("TheLoai")]
        public int IDTheLoai { get; set; }
        public TheLoai TheLoai { get; set; }

        [ForeignKey("NhaXuatBan")]
        public int IDNhaXuatBan { get; set; }
        public NhaXuatBan NhaXuatBan { get; set; }

        public string Anh { get; set; }
        public int NamXB { get; set; }

        public ICollection<CTPhieuNhap> CTPhieuNhaps { get; set; }
        public ICollection<CTPhieuXuat> CTPhieuXuats { get; set; }
    }
}
