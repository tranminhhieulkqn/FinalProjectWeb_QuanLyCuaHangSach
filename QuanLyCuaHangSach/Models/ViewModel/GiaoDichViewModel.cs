﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Models.ViewModel
{
    public class GiaoDichViewModel
    {
        public List<GiaoDich> GiaoDich { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
