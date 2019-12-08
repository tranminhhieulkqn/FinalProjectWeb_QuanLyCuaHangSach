using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models.ViewModel
{
    public class PhieuNhapViewModel
    {
        public PhieuNhap PhieuNhap { get; set; }
        //Nếu sách có sẵn thì không dùng
        public SachViewModel SachVM { get; set; }
        public IEnumerable<ApplicationUser> NhanVien { get; set; }
    }
}
