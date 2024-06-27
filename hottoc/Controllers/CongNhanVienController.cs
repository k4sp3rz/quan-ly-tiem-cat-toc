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
    public class CongNhanVienController : Controller
    {
        private Model1 db = new Model1();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = Session["IDNV"];
            if (id == null)
                filterContext.Result = RedirectToAction("Index", "Login");
            base.OnActionExecuting(filterContext);
        }

        // GET: CongNhanViens
        public ActionResult Index()
        {
            int id = (int)Session["IDNV"];
            var congNhanViens = db.CongNhanViens.Where(c => c.NhanVien.ID == id);
            return View(congNhanViens.ToList());
        }

        // GET: CongNhanViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNhanVien congNhanVien = db.CongNhanViens.Find(id);
            if (congNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(congNhanVien);
        }

        // GET: CongNhanViens/Create
        public ActionResult Create()
        {
            int id = (int)Session["IDNV"];
            ViewBag.NhanVien = db.NhanViens.Where(c => c.ID == id).FirstOrDefault();
            return View();
        }

        // POST: CongNhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDNV,NgayCong")] CongNhanVien congNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.CongNhanViens.Add(congNhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNV = new SelectList(db.NhanViens, "ID", "HoTen", congNhanVien.IDNV);
            return View(congNhanVien);
        }

        // GET: CongNhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNhanVien congNhanVien = db.CongNhanViens.Find(id);
            if (congNhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNV = new SelectList(db.NhanViens, "ID", "HoTen", congNhanVien.IDNV);
            return View(congNhanVien);
        }

        // POST: CongNhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDNV,NgayCong")] CongNhanVien congNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congNhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNV = new SelectList(db.NhanViens, "ID", "HoTen", congNhanVien.IDNV);
            return View(congNhanVien);
        }

        // GET: CongNhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNhanVien congNhanVien = db.CongNhanViens.Find(id);
            if (congNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(congNhanVien);
        }

        // POST: CongNhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongNhanVien congNhanVien = db.CongNhanViens.Find(id);
            db.CongNhanViens.Remove(congNhanVien);
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
