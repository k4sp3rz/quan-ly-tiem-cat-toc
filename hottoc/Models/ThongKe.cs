using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace hottoc.Models.ThongKe
{
    public class ChiTiet
    {
        public ChiTiet(string ten, int? tongSo, decimal? tongThu)
        {
            Ten = ten;
            TongSo = tongSo;
            TongThu = tongThu;
        }

        public ChiTiet(string ten, int? tongSo, string donViTinh, decimal? tongThu)
        {
            Ten = ten;
            TongSo = tongSo;
            DonViTinh = donViTinh;
            TongThu = tongThu;
        }

        public string Ten { get; set; }
        public int? TongSo { get; set; }
        public string DonViTinh { get; set; }
        public decimal? TongThu { get; set; }
    }

    public class DanhSachChiTiet
    {
        public DanhSachChiTiet() { }
        public DanhSachChiTiet(decimal tongThu, List<ChiTiet> chiTiet)
        {
            TongThu = tongThu;
            ChiTiet = chiTiet;
        }

        public decimal TongThu { get; set; }
        public List<ChiTiet> ChiTiet { get; set; } = new List<ChiTiet>();
    }

    public class ThongKeNgay
    {
        public ThongKeNgay() { }
        public ThongKeNgay(decimal tongDoanhThu, int tongKhacHang, int tongLichHen)
        {
            TongDoanhThu = tongDoanhThu;
            TongKhacHang = tongKhacHang;
            TongLichHen = tongLichHen;

            ChiTietDichVu = new DanhSachChiTiet();
            ChiTietSanPham = new DanhSachChiTiet();
        }

        public decimal TongDoanhThu { get; set; }
        public int TongKhacHang { get; set; }
        public int TongLichHen { get; set; }

        public DanhSachChiTiet ChiTietDichVu { get; set; } = new DanhSachChiTiet();
        public DanhSachChiTiet ChiTietSanPham { get; set; } = new DanhSachChiTiet();

        public static ThongKeNgay UseDB(Model1 db)
        {
            var self = new ThongKeNgay();

            db.HoaDons.Where(x => x.Ngay.Day == DateTime.Now.Day).ForEach(item =>
            {
                self.TongDoanhThu += item.ThanhTien;
                self.TongKhacHang += 1;
                db.ChiTietHoaDons.Where(x => x.IDHoaDon == item.ID)
                    .ForEach(x =>
                    {
                        var q = self.ChiTietSanPham;
                        if (x.Loai == "Dịch vụ")
                        {
                            q = self.ChiTietDichVu;
                        }

                        q.TongThu += (decimal)x.ThanhTien;
                        var isSet = q.ChiTiet.Where(
                            sp => sp.Ten == x.TenSP).FirstOrDefault().IfNotNull(sp =>
                            {
                                sp.TongSo += x.SoLuong;
                                sp.TongThu += x.ThanhTien;
                                return true;
                            }, false);
                        if (!isSet)
                        {
                            if (x.Loai == "Dịch vụ")
                                db.DichVus.Where(
                                sp => sp.TenDV == x.TenSP).FirstOrDefault().IfNotNull(sp =>
                                {
                                    q.ChiTiet.Add(
                                        new ChiTiet(x.TenSP, x.SoLuong, x.ThanhTien));
                                });
                            else
                                db.SanPhamKems.Where(
                                    sp => sp.TenSP == x.TenSP).FirstOrDefault().IfNotNull(sp =>
                                    {
                                        q.ChiTiet.Add(
                                            new ChiTiet(x.TenSP, x.SoLuong, x.Loai == "DV" ? "" : sp.DonViTinh, x.ThanhTien));
                                    });
                        }
                    });
            });

            self.TongLichHen = db.LichHens.Where(x => x.ThoiGianHen.Day == DateTime.Now.Day).Count();
            return self;
        }
    }

    public class ThongKeThang
    {
        public ThongKeThang() { }
        public ThongKeThang(decimal tongDoanhThu, int tongKhacHang, int tongLichHen)
        {
            TongDoanhThu = tongDoanhThu;
            TongKhacHang = tongKhacHang;
            TongLichHen = tongLichHen;

            ChiTietDichVu = new DanhSachChiTiet();
            ChiTietSanPham = new DanhSachChiTiet();
        }

        public decimal TongDoanhThu { get; set; }
        public int TongKhacHang { get; set; }
        public int TongLichHen { get; set; }

        public DanhSachChiTiet ChiTietDichVu { get; set; } = new DanhSachChiTiet();
        public DanhSachChiTiet ChiTietSanPham { get; set; } = new DanhSachChiTiet();

        public static ThongKeThang UseDB(Model1 db, int? monthGet = null)
        {
            var self = new ThongKeThang();

            int month = DateTime.Now.Month;
            if (monthGet.HasValue)
            {
                month = monthGet.Value;
            }

            db.HoaDons.Where(x => x.Ngay.Month == month).ForEach(item =>
            {
                self.TongDoanhThu += item.ThanhTien;
                self.TongKhacHang += 1;
                db.ChiTietHoaDons.Where(x => x.IDHoaDon == item.ID)
                    .ForEach(x =>
                    {
                        var q = self.ChiTietSanPham;
                        if (x.Loai == "Dịch vụ")
                        {
                            q = self.ChiTietDichVu;
                        }

                        q.TongThu += (decimal)x.ThanhTien;
                        var isSet = q.ChiTiet.Where(
                            sp => sp.Ten == x.TenSP).FirstOrDefault().IfNotNull(sp =>
                            {
                                sp.TongSo += x.SoLuong;
                                sp.TongThu += x.ThanhTien;
                                return true;
                            }, false);
                        if (!isSet)
                        {
                            if (x.Loai == "Dịch vụ")
                                db.DichVus.Where(
                                sp => sp.TenDV == x.TenSP).FirstOrDefault().IfNotNull(sp =>
                                {
                                    q.ChiTiet.Add(
                                        new ChiTiet(x.TenSP, x.SoLuong, x.ThanhTien));
                                });
                            else
                                db.SanPhamKems.Where(
                                    sp => sp.TenSP == x.TenSP).FirstOrDefault().IfNotNull(sp =>
                                    {
                                        q.ChiTiet.Add(
                                            new ChiTiet(x.TenSP, x.SoLuong, x.Loai == "DV" ? "" : sp.DonViTinh, x.ThanhTien));
                                    });
                        }
                    });
            });

            self.TongLichHen = db.LichHens.Where(x => x.ThoiGianHen.Month == month).Count();
            return self;
        }
    }

    public class ThongKeKhachHang
    {
        public class CustomerInfo
        {
            public string TenKH { get; set; }
            public int TongDV { get; set; }
            public decimal TongTienDV { get; set; }
            public int TongSP { get; set; }
            public decimal TongTienSP { get; set; }
        }

        public static List<CustomerInfo> GetDailyCustomerFrequency(Model1 db)
        {
            var self = new List<CustomerInfo>();
            db.HoaDons.Where(x => x.Ngay.Day == DateTime.Now.Day)
                .ForEach(x =>
                {
                    var isSet = self.Where(s => s.TenKH == x.TenKH).FirstOrDefault().IfNotNull(s =>
                    {
                        s.TongDV += x.TongSLDV;
                        s.TongTienDV += (int)x.TongTienDV;
                        s.TongSP += (int)x.TongSLSP;
                        s.TongTienSP += (int)x.TongTienSP;
                        return true;
                    }, false);

                    if (!isSet)
                    {
                        self.Add(
                            new CustomerInfo
                            {
                                TongDV = x.TongSLDV,
                                TongTienDV = (int)x.TongTienDV,
                                TongSP = (int)x.TongSLSP,
                                TongTienSP = (int)x.TongTienSP,
                            });
                    }
                });
            return self;
        }
        public static List<CustomerInfo> GetMonthlyCustomerFrequency(Model1 db, int? monthGet = null)
        {
            var self = new List<CustomerInfo>();
            int month = DateTime.Now.Month;
            if (monthGet.HasValue)
            {
                month = monthGet.Value;
            }

            db.HoaDons.Where(x => x.Ngay.Month == month)
                .ForEach(x =>
                {
                    var isSet = self.Where(s => s.TenKH == x.TenKH).FirstOrDefault().IfNotNull(s =>
                    {
                        s.TongDV += x.TongSLDV;
                        s.TongTienDV += (int)x.TongTienDV;
                        s.TongSP += (int)x.TongSLSP;
                        s.TongTienSP += (int)x.TongTienSP;
                        return true;
                    }, false);

                    if (!isSet)
                    {
                        self.Add(
                            new CustomerInfo
                            {
                                TongDV = x.TongSLDV,
                                TongTienDV = (int)x.TongTienDV,
                                TongSP = (int)x.TongSLSP,
                                TongTienSP = (int)x.TongTienSP,
                            });
                    }
                });
            return self;
        }
    }
}