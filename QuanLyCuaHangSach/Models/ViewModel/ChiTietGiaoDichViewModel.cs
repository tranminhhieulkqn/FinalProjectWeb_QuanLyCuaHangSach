using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models.ViewModel
{
    public class ChiTietGiaoDichViewModel
    {
        public GiaoDich GiaoDich { get; set; }
        public List<ApplicationUser> NguoiBan { get; set; }
        public List<Sach> Sach { get; set; }
    }
}
