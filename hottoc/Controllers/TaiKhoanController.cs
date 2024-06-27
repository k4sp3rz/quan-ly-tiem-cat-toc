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
    public class TaiKhoanController : Controller
    {
        private Model1 db = new Model1();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = Session["IDNV"];
            if (id == null)
                filterContext.Result = RedirectToAction("Index", "Login");
            else
            {
                var role = Session["Role"];
                if (!role.Equals("Chủ tiệm"))
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(filterContext);
        }

        // GET: TaiKhoan
        public ActionResult Index()
        {
            var dangNhaps = db.DangNhaps.Include(d => d.NhanVien);
            return View(dangNhaps.ToList());
        }

        // GET: TaiKhoan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangNhap dangNhap = db.DangNhaps.Find(id);
            if (dangNhap == null)
            {
                return HttpNotFound();
            }
            return View(dangNhap);
        }

        // GET: TaiKhoan/Create
        public ActionResult Create()
        {
            ViewBag.IDNV = new SelectList(db.NhanViens, "ID", "HoTen");
            return View();
        }

        // POST: TaiKhoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,IDNV")] DangNhap dangNhap)
        {
            if (ModelState.IsValid)
            {
                db.DangNhaps.Add(dangNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNV = new SelectList(db.NhanViens, "ID", "HoTen", dangNhap.IDNV);
            return View(dangNhap);
        }

        // GET: TaiKhoan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangNhap dangNhap = db.DangNhaps.Find(id);
            if (dangNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNV = new SelectList(db.NhanViens, "ID", "HoTen", dangNhap.IDNV);
            return View(dangNhap);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,IDNV")] DangNhap dangNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dangNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNV = new SelectList(db.NhanViens, "ID", "HoTen", dangNhap.IDNV);
            return View(dangNhap);
        }

        // GET: TaiKhoan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangNhap dangNhap = db.DangNhaps.Find(id);
            if (dangNhap == null)
            {
                return HttpNotFound();
            }
            return View(dangNhap);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DangNhap dangNhap = db.DangNhaps.Find(id);
            db.DangNhaps.Remove(dangNhap);
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
