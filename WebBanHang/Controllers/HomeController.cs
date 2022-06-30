using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext db = new StoreContext();
        public ActionResult Index(string thuonghieu, string search)
        {
            var listsp = new List<tb_SANPHAM>();

            if (thuonghieu != null && thuonghieu != "")
            {
                listsp = db.tb_SANPHAM.Where(p => p.TRANGTHAI == true && p.HANG == thuonghieu).ToList();
                if (listsp == null)
                {
                    listsp = db.tb_SANPHAM.Where(p => p.TRANGTHAI == true).ToList();
                }
            }
            else if (search != null && search != "")
            {
                listsp = db.tb_SANPHAM.Where(p => p.TRANGTHAI == true).Where(p=> p.TENANPHAM.Contains(search) || p.HANG.Contains(search)).ToList();
            }
            else
            {
                listsp = db.tb_SANPHAM.Where(p => p.TRANGTHAI == true).ToList();
            }

            ViewBag.count = listsp.Count();
            return View(listsp);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}