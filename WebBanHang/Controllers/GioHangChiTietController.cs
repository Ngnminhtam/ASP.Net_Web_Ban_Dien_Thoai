using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class GioHangChiTietController : Controller
    {
        private StoreContext db = new StoreContext();

        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        // GET: GioHangChiTiet
        public ActionResult Index()
        {
            DateTime ngay = DateTime.Now.Date;
            DateTime now = DateTime.Now;
            int dayOfWeek = (int)now.DayOfWeek;
            DateTime tuan = now.AddDays(-(int)now.DayOfWeek);
            DateTime thang = GetFirstDayOfMonth(now);


            var listall = db.tb_GIOHANGCHITIET.Include(t => t.tb_GIOHANG).OrderByDescending(p => p.tb_GIOHANG.NGAYDAT).Include(t => t.tb_SANPHAM);
            ViewBag.tsdh = listall.Count();
            ViewBag.dt = listall.Where(p=> p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.THANHTIEN);
            ViewBag.slsp = listall.Where(p => p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.SOLUONG);
            ViewBag.cxl = listall.Where(p=> p.tb_GIOHANG.TRANGTHAI == "Chờ xử lý").Count();

            var listngay = db.tb_GIOHANGCHITIET.Include(t => t.tb_GIOHANG).Where(p=> p.tb_GIOHANG.NGAYDAT >= ngay).OrderByDescending(p => p.tb_GIOHANG.NGAYDAT).Include(t => t.tb_SANPHAM);
            ViewBag.listngay = listngay;
            ViewBag.tsdhn = listngay.Count();
            ViewBag.dtn = listngay.Where(p => p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.THANHTIEN);
            ViewBag.slspn = listngay.Where(p => p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.SOLUONG);
            ViewBag.cxln = listngay.Where(p => p.tb_GIOHANG.TRANGTHAI == "Chờ xử lý").Count();

            var listtuan = db.tb_GIOHANGCHITIET.Include(t => t.tb_GIOHANG).Where(p => p.tb_GIOHANG.NGAYDAT >= tuan).OrderByDescending(p => p.tb_GIOHANG.NGAYDAT).Include(t => t.tb_SANPHAM);
            ViewBag.listtuan = listtuan;
            ViewBag.tsdhtu = listtuan.Count();
            ViewBag.dttu = listtuan.Where(p => p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.THANHTIEN);
            ViewBag.slsptu = listtuan.Where(p => p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.SOLUONG);
            ViewBag.cxltu = listtuan.Where(p => p.tb_GIOHANG.TRANGTHAI == "Chờ xử lý").Count();

            var listthang = db.tb_GIOHANGCHITIET.Include(t => t.tb_GIOHANG).Where(p => p.tb_GIOHANG.NGAYDAT >= thang).OrderByDescending(p => p.tb_GIOHANG.NGAYDAT).Include(t => t.tb_SANPHAM);
            ViewBag.listthang = listthang;
            ViewBag.tsdhm = listthang.Count();
            ViewBag.dtm = listthang.Where(p => p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.THANHTIEN);
            ViewBag.slspm = listthang.Where(p => p.tb_GIOHANG.THANHTOAN == true).Sum(p => p.SOLUONG);
            ViewBag.cxlm = listthang.Where(p => p.tb_GIOHANG.TRANGTHAI == "Chờ xử lý").Count();


            return View(listall);
        }

        public ActionResult GiaoHang (int id)
        {
            tb_GIOHANG gh = db.tb_GIOHANG.FirstOrDefault(p => p.IDGIOHANG == id);

            gh.TRANGTHAI = "Đang giao hàng";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Huy(int id)
        {
            tb_GIOHANG gh = db.tb_GIOHANG.FirstOrDefault(p => p.IDGIOHANG == id);

            gh.TRANGTHAI = "Đã hủy";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Hoantat(int id)
        {
            tb_GIOHANG gh = db.tb_GIOHANG.FirstOrDefault(p => p.IDGIOHANG == id);
            gh.TRANGTHAI = "Hoàn tất";
            gh.THANHTOAN = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: GioHangChiTiet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANGCHITIET tb_GIOHANGCHITIET = db.tb_GIOHANGCHITIET.Find(id);
            if (tb_GIOHANGCHITIET == null)
            {
                return HttpNotFound();
            }
            return View(tb_GIOHANGCHITIET);
        }

        // GET: GioHangChiTiet/Create
        public ActionResult Create()
        {
            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "Id");
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENANPHAM");
            return View();
        }

        // POST: GioHangChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGIOHANG,IDSANPHAM,SOLUONG,THANHTIEN")] tb_GIOHANGCHITIET tb_GIOHANGCHITIET)
        {
            if (ModelState.IsValid)
            {
                db.tb_GIOHANGCHITIET.Add(tb_GIOHANGCHITIET);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "Id", tb_GIOHANGCHITIET.IDGIOHANG);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENANPHAM", tb_GIOHANGCHITIET.IDSANPHAM);
            return View(tb_GIOHANGCHITIET);
        }

        // GET: GioHangChiTiet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANGCHITIET tb_GIOHANGCHITIET = db.tb_GIOHANGCHITIET.Find(id);
            if (tb_GIOHANGCHITIET == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "Id", tb_GIOHANGCHITIET.IDGIOHANG);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENANPHAM", tb_GIOHANGCHITIET.IDSANPHAM);
            return View(tb_GIOHANGCHITIET);
        }

        // POST: GioHangChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGIOHANG,IDSANPHAM,SOLUONG,THANHTIEN")] tb_GIOHANGCHITIET tb_GIOHANGCHITIET)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_GIOHANGCHITIET).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "Id", tb_GIOHANGCHITIET.IDGIOHANG);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENANPHAM", tb_GIOHANGCHITIET.IDSANPHAM);
            return View(tb_GIOHANGCHITIET);
        }

        // GET: GioHangChiTiet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANGCHITIET tb_GIOHANGCHITIET = db.tb_GIOHANGCHITIET.Find(id);
            if (tb_GIOHANGCHITIET == null)
            {
                return HttpNotFound();
            }
            return View(tb_GIOHANGCHITIET);
        }

        // POST: GioHangChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_GIOHANGCHITIET tb_GIOHANGCHITIET = db.tb_GIOHANGCHITIET.Find(id);
            db.tb_GIOHANGCHITIET.Remove(tb_GIOHANGCHITIET);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
