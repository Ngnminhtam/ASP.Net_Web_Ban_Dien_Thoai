using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class Giohang
    {
        StoreContext data = new StoreContext();
        [Display(Name = "ID sản phẩm")]
        public int idSP { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string tenSP { get; set; }

        [Display(Name = "Hãng")]
        public string hang { get; set; }

        [Display(Name = "Hình ảnh")]
        public string hinh { get; set; }

        [Display(Name = "Giá bán")]
        public Double giaBan { get; set; }

        [Display(Name = "Số lượng")]
        public int iSoluong { get; set; }

        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return iSoluong * giaBan; }
        }
        public Giohang(int id)
        {
            idSP = id;
            tb_SANPHAM sanpham = data.tb_SANPHAM.Single(p => p.IDSANPHAM == idSP);
            tenSP = sanpham.TENANPHAM;
            hinh = sanpham.HINHANH;
            hang = sanpham.HANG;
            giaBan = double.Parse(sanpham.GIABAN.ToString());
            iSoluong = 1;
        }
    }
}