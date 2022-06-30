using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class DanhGiaController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: DanhGia
        public ActionResult Index()
        {
            var list = db.tb_SANPHAM.ToList();
            return View(list);
        }
    }
}