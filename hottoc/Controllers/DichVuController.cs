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
    public class DichVuController : Controller
    {
        private Model1 db = new Model1();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<string> needPerms = new List<string> { "Create", "Edit", "Delete", "DeleteConfirmed" };
            var id = Session["IDNV"];
            if (id == null)
                filterContext.Result = RedirectToAction("Index", "Login");
            else
            {
                var role = Session["Role"];
                if (needPerms.Contains(filterContext.ActionDescriptor.ActionName) && !role.Equals("Chủ tiệm"))
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(filterContext);
        }

        // GET: DichVu
        public ActionResult Index()
        {
            var dichVus = db.DichVus.Include(d => d.HinhAnh);
            return View(dichVus.ToList());
        }

        public ActionResult Calculate(string q, int sl)
        {
            var sp = db.DichVus.Where(x => x.TenDV.Equals(q)).FirstOrDefault();
            if (sp == null)
                return HttpNotFound();

            return Content((sp.Gia * sl).ToString());
        }

        // GET: DichVu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichVu dichVu = db.DichVus.Find(id);
            if (dichVu == null)
            {
                return HttpNotFound();
            }
            return View(dichVu);
        }

        // GET: DichVu/Create
        public ActionResult Create()
        {
            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan");
            return View();
        }

        // POST: DichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenDV,Gia,MoTa,IDHinh")] DichVu dichVu)
        {
            if (ModelState.IsValid)
            {
                db.DichVus.Add(dichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan", dichVu.IDHinh);
            return View(dichVu);
        }

        // GET: DichVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichVu dichVu = db.DichVus.Find(id);
            if (dichVu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan", dichVu.IDHinh);
            return View(dichVu);
        }

        // POST: DichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenDV,Gia,MoTa,IDHinh")] DichVu dichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHinh = new SelectList(db.HinhAnhs, "ID", "DuongDan", dichVu.IDHinh);
            return View(dichVu);
        }

        // GET: DichVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichVu dichVu = db.DichVus.Find(id);
            if (dichVu == null)
            {
                return HttpNotFound();
            }
            return View(dichVu);
        }

        // POST: DichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DichVu dichVu = db.DichVus.Find(id);
            db.DichVus.Remove(dichVu);
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
