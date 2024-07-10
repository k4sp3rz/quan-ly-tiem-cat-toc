using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hottoc.Models;

namespace hottoc.Controllers
{
    public class HoaDonController : Controller
    {
        private Model1 db = new Model1();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = Session["IDNV"];
            if (id == null)
                filterContext.Result = RedirectToAction("Index", "Login");
            base.OnActionExecuting(filterContext);
        }

        // GET: HoaDon
        public ActionResult Index()
        {
            return View(db.HoaDons.ToList());
        }

        // GET: HoaDon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: HoaDon/Create
        public ActionResult Create()
        {
            // Gán dữ liệu cho ViewBag.TenDV trong GET
            ViewBag.TenDV = new SelectList(db.DichVus, "ID", "TenDV");
            ViewBag.TenSP = new SelectList(db.SanPhamKems, "ID", "TenSP");
            return View();
        }

        // POST: HoaDon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NhanVien,Ngay,TenKH,SDT,TenSP,TongSLSP,TongTienSP,TongSLDV,TenDV,ThanhTien,KhachTra,ThoiLai")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Gán lại dữ liệu cho ViewBag.TenDV trong trường hợp ModelState không hợp lệ
            ViewBag.TenDV = new SelectList(db.DichVus, "ID", "TenDV");
            ViewBag.TenSP = new SelectList(db.SanPhamKems, "ID", "TenSP");
            return View(hoaDon);
        }


        // GET: HoaDon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NhanVien,Ngay,TenKH,SDT,TongSLSP,TongTienSP,TongSLDV,TenDV,ThanhTien,KhachTra,ThoiLai")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hoaDon);
        }

        // GET: HoaDon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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
