using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.VNPay;

namespace WebBanHang.Controllers
{
    public class GioHangController : Controller
    {
        // GET: Giohang
        StoreContext data = new StoreContext();

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        //public ActionResult LichSuDatHangAdmin()
        //{
        //    List<tb_DonHang> donhang = data.tb_DonHangs.ToList();
        //    List<tb_DonHang_SanPham> donhangsanpham = data.tb_DonHang_SanPhams.ToList();
        //    List<tb_SanPham> sanpham = data.tb_SanPhams.ToList();
        //    var lstAll = from e in donhang
        //                 join d in donhangsanpham on e.idDonHang equals d.idDonHang into table1
        //                 from d in table1.ToList()
        //                 join i in sanpham on d.idSP equals i.idSP into table2
        //                 from i in table2.ToList()
        //                 select new ViewModel
        //                 {
        //                     donhang = e,
        //                     donhangsanpham = d,
        //                     sanpham = i
        //                 };
        //    return View(lstAll);
        //}

        public ActionResult LichSuDatHang()
        {
            string id = (string)Session["Id"];
            List<tb_GIOHANG> giohang = data.tb_GIOHANG.Where(p => p.Id == id).ToList();
            List<tb_GIOHANGCHITIET> giohangcitiet = data.tb_GIOHANGCHITIET.ToList();
            List<tb_SANPHAM> sanpham = data.tb_SANPHAM.ToList();

            var lstAll = from e in giohang
                         join d in giohangcitiet on e.IDGIOHANG equals d.IDGIOHANG into table1
                         from d in table1.ToList()
                         join i in sanpham on d.IDSANPHAM equals i.IDSANPHAM into table2
                         from i in table2.ToList()
                         select new Donhang
                         {
                             gh = e,
                             ghct = d,
                             sp = i
                         };

            return View(lstAll.OrderByDescending(p=> p.gh.IDGIOHANG));
        }

        /*public ActionResult danhGia(int idSP, int idDH, int sao)
        {
            var donHang = data.tb_DonHang_SanPhams.First(m => m.idDonHang == idDH && m.idSP == idSP);
            var sanpham = data.tb_SanPhams.First(m => m.idSP == idSP);
            sanpham.danhGia = (sanpham.soNguoiDanhGia * sanpham.danhGia + sao) / (sanpham.soNguoiDanhGia + 1);
            sanpham.soNguoiDanhGia += 1;

            donHang.danhGia = sao;
            UpdateModel(donHang);
            UpdateModel(sanpham);
            data.SubmitChanges();
            tb_TaiKhoan tk = (tb_TaiKhoan)Session["Taikhoan"];
            return RedirectToAction("LichSuDatHang/" + tk.idTaiKhoan);
        }*/

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                Session["soluong"] = TongSoLuong();
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong += 1;
                Session["soluong"] = TongSoLuong();
                return Redirect(strURL);
            }
        }

        public ActionResult Muangay(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong += 1;
                return Redirect(@Url.Action("GioHang", "GioHang"));
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(p => p.iSoluong);
            }
            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(p => p.dThanhtien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            Session["soluong"] = TongSoLuong();
            return View(lstGiohang);
        }
         
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            Session["soluong"] = TongSoLuong();
            return PartialView();
        }

        public ActionResult XoaGiohang(int id)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(p => p.idSP == id);
                Session["soluong"] = TongSoLuong();
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);
            if (sanpham != null)
            {
                Session["soluong"] = TongSoLuong();
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            Session["soluong"] = TongSoLuong();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            tb_GIOHANG dh = new tb_GIOHANG();
            AspNetUsers kh = (AspNetUsers)Session["TaiKhoan"];
            tb_SANPHAM s = new tb_SANPHAM();

            List<Giohang> gh = Laygiohang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["ngayGiao"]);

            var httt = collection["httt"].ToString();

            dh.Id = kh.Id;
            dh.NGAYDAT = DateTime.Now;
            dh.NGAYGIAO = DateTime.Parse(ngaygiao);
            dh.THANHTIEN = (float)TongTien();
            dh.TRANGTHAI = "Chờ xử lý";
            dh.THANHTOAN = false;

            data.tb_GIOHANG.Add(dh);
            data.SaveChanges();

            foreach (var item in gh)
            {
                tb_GIOHANGCHITIET ctdh = new tb_GIOHANGCHITIET();
                ctdh.IDGIOHANG = dh.IDGIOHANG;
                ctdh.IDSANPHAM = item.idSP;
                ctdh.SOLUONG = item.iSoluong;
                ctdh.THANHTIEN= item.dThanhtien;
                data.SaveChanges();

                data.tb_GIOHANGCHITIET.Add(ctdh);
            }
            data.SaveChanges();
            string ttstr = TongTien().ToString() + "00";
            Session["Giohang"] = null;
            Session["soluong"] = TongSoLuong();
            Session["IDDONHANG"] = dh.IDGIOHANG;

            string noidung = kh.Name + kh.PhoneNumber + dh.IDGIOHANG + dh.NGAYDAT;
            

            if(httt == "1")
            {
                return RedirectToAction("XacnhanDonhang", "GioHang");
            }
            else
            {
                return RedirectToAction("Payment", new { @sotien = ttstr, @noidung = noidung });
            }
        }

        public ActionResult Thanhtoan()
        {
            return View();
        }

        public ActionResult XacnhanDonhang()
        {
            AspNetUsers tk = (AspNetUsers)Session["Taikhoan"];
            //Session["htThanhToan"] = tk.Name;
            Session["sdtThanhToan"] = tk.PhoneNumber;
            Session["timeThanhToan"] = DateTime.Now.Date;
            return View();
        }

        public ActionResult Payment(string sotien, string noidung)
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.0.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", sotien); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", "NCB"); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", noidung); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }

        public ActionResult PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        StoreContext db = new StoreContext();
                        int id = (int)Session["IDDONHANG"];
                        tb_GIOHANG gh = db.tb_GIOHANG.Find(id);
                        gh.THANHTOAN = true;
                        db.SaveChanges();
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }
    }
}