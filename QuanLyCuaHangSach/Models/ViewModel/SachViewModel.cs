using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models.ViewModel
{
    public class SachViewModel
    {
        public Sach Sach { get; set; }
        public IEnumerable<TacGia> TacGia { get; set; }
        public IEnumerable<TheLoai> TheLoai { get; set; }
        public IEnumerable<NhaXuatBan> NhaXuatBan { get; set; }
    }
}
