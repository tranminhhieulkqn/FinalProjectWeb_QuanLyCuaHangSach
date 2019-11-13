using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class NhaXuatBan
    {
        [Key]
        public int IDNhaXuatBan { get; set; }
        public String TenNhaXuatBan { get; set; }
        public String DiaChi { get; set; }
        public String Email { get; set; }
        public String ThongTinNguoiDaiDien { get; set; }

        public ICollection<Sach> Sachs { get; set; }

    }
}
