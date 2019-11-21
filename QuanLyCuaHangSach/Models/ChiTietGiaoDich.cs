using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class ChiTietGiaoDich
    {
        [Key]
        public int IDCTGiaoDich { get; set; }

        [ForeignKey("GiaoDich")]
        public int IDGiaoDich { get; set; }
        public virtual GiaoDich GiaoDich { get; set; }


        [ForeignKey("Sach")]
        public int IDSach { get; set; }
        public virtual Sach Sach { get; set; }
        public int SoLuongMua { get; set; }
        
    }
}
