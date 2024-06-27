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
    public class SanPhamKemController : Controller
    {
        private Model1 db = new Model1();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = Session["IDNV"];
            if (id == null)
                filterContext.Result = RedirectToAction("Index", "Login");
            base.OnActionExecuting(filterContext);
        }

        // GET: SanPhamKem
        public ActionResult Index()
        {
            var sanPhamKems = db.SanPhamKems.Include(s => s.HinhAnh);
            return View(sanPhamKems.ToList());
        }

        // GET: SanPhamKem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKem sanPhamKem = db.SanPhamKems.Find(id);
            if (sanPhamKem == null)
            {
                return HttpNotFound();
            }
            return View(sanPhamKem);
        }

        // GET: SanPhamKem/Create
        public ActionResult Create()
        {
            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan");
            return View();
        }

        // POST: SanPhamKem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenSP,MoTa,DonViTinh,Gia,IDHinh")] SanPhamKem sanPhamKem)
        {
            if (ModelState.IsValid)
            {
                db.SanPhamKems.Add(sanPhamKem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan", sanPhamKem.IDHinh);
            return View(sanPhamKem);
        }

        // GET: SanPhamKem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKem sanPhamKem = db.SanPhamKems.Find(id);
            if (sanPhamKem == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan", sanPhamKem.IDHinh);
            return View(sanPhamKem);
        }

        // POST: SanPhamKem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenSP,MoTa,DonViTinh,Gia,IDHinh")] SanPhamKem sanPhamKem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPhamKem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan", sanPhamKem.IDHinh);
            return View(sanPhamKem);
        }

        // GET: SanPhamKem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKem sanPhamKem = db.SanPhamKems.Find(id);
            if (sanPhamKem == null)
            {
                return HttpNotFound();
            }
            return View(sanPhamKem);
        }

        // POST: SanPhamKem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPhamKem sanPhamKem = db.SanPhamKems.Find(id);
            db.SanPhamKems.Remove(sanPhamKem);
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
