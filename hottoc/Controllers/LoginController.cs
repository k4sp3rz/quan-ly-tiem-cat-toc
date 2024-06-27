using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hottoc.Models;

namespace hottoc.Controllers
{
    public class LoginController : Controller
    {
        private readonly Model1 database = new Model1();

        // GET: Login
        public ActionResult Index()
        {
            var Id = Session["IDNV"];
            if (Id != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(DangNhap user)
        {
            var check = database.DangNhaps.Where(s =>
                                                 s.Username == user.Username &&
                                                 s.Password == user.Password
                                                ).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Thông tin đăng nhập bị sai. Vui lòng kiểm tra";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["IDNV"] = check.IDNV;
                Session["Username"] = check.Username;
                Session["Role"] = check.NhanVien.ChucVu.TenChucVu;
                return RedirectToAction("Index", "Home");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}