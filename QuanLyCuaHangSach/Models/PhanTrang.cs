using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models
{
    public class PhanTrang
    {
        public int TongSoSach { get; set; }
        public int SoSachTrenTrang { get; set; }
        public int TrangHienTai { get; set; }
        public int totalPage => (int)Math.Ceiling((decimal)TongSoSach / SoSachTrenTrang);
        public string urlParam { get; set; }
    }
}
