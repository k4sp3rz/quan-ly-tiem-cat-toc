using hottoc.Models;
using hottoc.Models.ThongKe;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hottoc.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        private void checkDb()
        {
            var checkdb = db.DangNhaps.Where(x => x.Username == "admin").FirstOrDefault();
            if (checkdb == null)
            {
                ChucVu chucvu = db.ChucVus.Add(new ChucVu
                {
                    TenChucVu = "Chủ tiệm",
                    HeSoLuong = 1,
                    LuongCoBan = 1
                });
                NhanVien nhanvien = db.NhanViens.Add(new NhanVien
                {
                    HoTen = "Nguyen Van A",
                    SDT = 123456789,
                    ChucVuID = chucvu.ID
                });
                db.DangNhaps.Add(new DangNhap
                {
                    IDNV = nhanvien.ID,
                    Username = "admin",
                    Password = "123123"
                });

                db.SanPhamKems.AddRange(new List<SanPhamKem>
                {
                    new SanPhamKem
                    {
                        TenSP = "testSP1",
                        MoTa = "motaSP",
                        DonViTinh = "Chai",
                        Gia = 10000
                    },
                    new SanPhamKem
                    {
                        TenSP = "testSP2",
                        MoTa = "motaSP2",
                        DonViTinh = "Chai",
                        Gia = 10000
                    }
                });

                db.DichVus.AddRange(new List<DichVu>
                {
                    new DichVu {
                        TenDV = "Cắt tóc",
                        MoTa = "motaSP1",
                        Gia = 49000
                    },
                    new DichVu {
                        TenDV = "Uốn tóc",
                        MoTa = "motaSP12",
                        Gia = 49000
                    }
                });

                db.KhachHangs.Add(new KhachHang
                {
                    HoTen = "Võ Văn B",
                    SDT = 123465789,
                    Loai = "Thường"
                });
                db.SaveChanges();
            }
        }
        public ActionResult Index()
        {
            checkDb();
            Session["IDNV"] = 0;
            Session["TenNV"] = "bypass";
            Session["Role"] = "Chủ tiệm";
            Session["CanEdit"] = true;

            var s = ThongKeNgay.UseDB(db);
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}