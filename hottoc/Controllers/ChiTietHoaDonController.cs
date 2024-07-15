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
    public class ChiTietHoaDonController : Controller
    {
        private Model1 db = new Model1();

        // GET: ChiTietHoaDon
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                var chiTietHoaDons = db.ChiTietHoaDons.Include(c => c.HoaDon);
                return View(chiTietHoaDons.ToList());
            }
            
            int cId = int.Parse(id);
            var hoadon = db.HoaDons.Where(x => x.ID.Equals(cId)).FirstOrDefault();
            if (hoadon == null)
                return HttpNotFound();

            ViewBag.HoaDon = hoadon;
            return View(hoadon.ChiTietHoaDons.ToList());
        }

        // GET: ChiTietHoaDon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDon/Create
        public ActionResult Create()
        {
            ViewBag.IDHoaDon = new SelectList(db.HoaDons, "ID", "NhanVien");
            return View();
        }

        // POST: ChiTietHoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDHoaDon,Loai,TenSP,SoLuong,ThanhTien")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDons.Add(chiTietHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHoaDon = new SelectList(db.HoaDons, "ID", "NhanVien", chiTietHoaDon.IDHoaDon);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHoaDon = new SelectList(db.HoaDons, "ID", "NhanVien", chiTietHoaDon.IDHoaDon);
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDHoaDon,Loai,TenSP,SoLuong,ThanhTien")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHoaDon = new SelectList(db.HoaDons, "ID", "NhanVien", chiTietHoaDon.IDHoaDon);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            db.ChiTietHoaDons.Remove(chiTietHoaDon);
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
