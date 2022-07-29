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
    public class SanPhamController : Controller
    {
        private StoreContext db = new StoreContext();

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

        // GET: SanPham
        public ActionResult Index()
        {
            return View(db.tb_SANPHAM.ToList());
        }

        // GET: SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM);
        }

        // GET: SanPham/Create
        public ActionResult Create()
        {
            List<string> listhang = new List<string>();
            listhang.Add("Samsung");
            listhang.Add("Apple");
            listhang.Add("Xiaomi");
            listhang.Add("Nokia");
            listhang.Add("Google");
            listhang.Add("Huawei");
            listhang.Add("Oppo");
            listhang.Add("Sony");
            listhang.Add("Vivo");
            ViewBag.HANG = new SelectList(listhang);

            ViewBag.listsanpham = db.tb_SANPHAM.ToList();

            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSANPHAM,TENANPHAM,HANG,GIABAN,HINHANH,MOTA")] tb_SANPHAM tb_SANPHAM)
        {
            if (ModelState.IsValid)
            {
                List<string> listhang = new List<string>();
                listhang.Add("Samsung");
            listhang.Add("Apple");
            listhang.Add("Xiaomi");
            listhang.Add("Nokia");
            listhang.Add("Google");
            listhang.Add("Huawei");
            listhang.Add("Oppo");
            listhang.Add("Sony");
            listhang.Add("Vivo");
                ViewBag.HANG = new SelectList(listhang, tb_SANPHAM.HANG);
                tb_SANPHAM.TRANGTHAI = true;
                db.tb_SANPHAM.Add(tb_SANPHAM);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(tb_SANPHAM);
        }

        // GET: SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }

            ViewBag.listsanpham = db.tb_SANPHAM.ToList();
            return View(tb_SANPHAM);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSANPHAM,TENANPHAM,HANG,GIABAN,HINHANH,MOTA,TRANGTHAI")] tb_SANPHAM tb_SANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_SANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(tb_SANPHAM);
        }

        // GET: SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            db.tb_SANPHAM.Remove(tb_SANPHAM);
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
