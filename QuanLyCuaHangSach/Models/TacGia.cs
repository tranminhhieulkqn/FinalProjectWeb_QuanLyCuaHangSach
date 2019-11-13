using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class TacGia
    {
        [Key]
        public int IDTacGia { get; set; }
        public String TenTacGia { get; set; }
        public String Website { get; set; }
        public String GhiChu { get; set; }

        public ICollection<Sach> Sachs { get; set; }
    }
}
