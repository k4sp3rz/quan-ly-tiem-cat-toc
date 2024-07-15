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
            ViewBag.NhanVien = db.NhanViens.Select(n => new SelectListItem
            {
                Text = n.HoTen,
                Value = n.ID.ToString()
            }).ToList();

            ViewBag.KhachHang = db.KhachHangs.Select(n => new SelectListItem
            {
                Text = n.HoTen,
                Value = n.ID.ToString()
            }).ToList();

            ViewBag.DanhSachDV = db.DichVus.Select(n => new SelectListItem
            {
                Text = n.TenDV,
                Value = n.ID.ToString()
            }).ToList();

            ViewBag.DanhSachSP = db.SanPhamKems.Select(n => new SelectListItem
            {
                Text = n.TenSP,
                Value = n.ID.ToString()
            }).ToList();

            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HoaDon rawHoaDon)
        {
            if (rawHoaDon == null) return View("Error");

            if (rawHoaDon.DanhSachSP != null)
            {
                foreach (var (idSP, slSP) in rawHoaDon.DanhSachSP.Zip(rawHoaDon.SoLuongSP, (n, c) => (n, c)))
                {
                    var sp = db.SanPhamKems.Where(x => x.ID == idSP).First();
                    db.ChiTietHoaDons.Add(new ChiTietHoaDon
                    {
                        IDHoaDon = rawHoaDon.ID,
                        Loai = "Sản phẩm kèm",
                        SoLuong = slSP,
                        ThanhTien = sp.Gia * slSP,
                        TenSP = sp.TenSP,
                    });
                }

                foreach (var (idDV, slDV) in rawHoaDon.DanhSachDV.Zip(rawHoaDon.SoLuongDV, (n, c) => (n, c)))
                {
                    var dv = db.DichVus.Where(x => x.ID == idDV).First();
                    db.ChiTietHoaDons.Add(new ChiTietHoaDon
                    {
                        IDHoaDon = rawHoaDon.ID,
                        Loai = "Dịch vụ",
                        SoLuong = slDV,
                        ThanhTien = dv.Gia * slDV,
                        TenSP = dv.TenDV,
                    });
                }
            }

            var idNV = int.Parse(rawHoaDon.NhanVien);
            rawHoaDon.NhanVien = db.NhanViens.Where(x => x.ID == idNV).First().HoTen;
            rawHoaDon.TongSLDV = rawHoaDon.SoLuongDV.Count;
            rawHoaDon.TongSLSP = rawHoaDon.SoLuongSP.Count;

            if (ModelState.IsValid)
            {
                db.HoaDons.Add(rawHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rawHoaDon);
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
