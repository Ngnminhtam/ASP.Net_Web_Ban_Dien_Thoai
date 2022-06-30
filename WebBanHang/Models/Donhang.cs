using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class Donhang
    {
        public tb_GIOHANG gh { get; set; }
        public tb_GIOHANGCHITIET ghct { get; set; }
        public  tb_SANPHAM sp { get; set; }
    }
}