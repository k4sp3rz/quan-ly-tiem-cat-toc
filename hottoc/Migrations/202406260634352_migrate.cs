namespace hottoc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietHoaDon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDHoaDon = c.Int(),
                        TenSP = c.String(maxLength: 255),
                        SoLuong = c.Int(),
                        ThanhTien = c.Decimal(precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HoaDon", t => t.IDHoaDon)
                .Index(t => t.IDHoaDon);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NhanVien = c.String(nullable: false, maxLength: 255),
                        Ngay = c.DateTime(nullable: false, storeType: "date"),
                        TenKH = c.String(maxLength: 255),
                        SDT = c.Int(),
                        TongSLSP = c.Int(),
                        TongTienSP = c.Int(),
                        TongSLDV = c.Int(nullable: false),
                        TenDV = c.String(nullable: false, maxLength: 255),
                        ThanhTien = c.Decimal(nullable: false, precision: 18, scale: 0),
                        KhachTra = c.Decimal(nullable: false, precision: 18, scale: 0),
                        ThoiLai = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ChucVu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HeSoLuong = c.Decimal(precision: 18, scale: 0),
                        LuongCoBan = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NhanVien",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HoTen = c.String(nullable: false, maxLength: 255),
                        SDT = c.Int(),
                        ChucVuID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChucVu", t => t.ChucVuID)
                .Index(t => t.ChucVuID);
            
            CreateTable(
                "dbo.CongNhanVien",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDNV = c.Int(nullable: false),
                        NgayCong = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NhanVien", t => t.IDNV)
                .Index(t => t.IDNV);
            
            CreateTable(
                "dbo.DangNhap",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Password = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        IDNV = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NhanVien", t => t.IDNV)
                .Index(t => t.IDNV);
            
            CreateTable(
                "dbo.DichVu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenDV = c.String(nullable: false, maxLength: 255),
                        Gia = c.Decimal(nullable: false, precision: 18, scale: 0),
                        MoTa = c.String(maxLength: 255),
                        IDHinh = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HinhAnh", t => t.IDHinh)
                .Index(t => t.IDHinh);
            
            CreateTable(
                "dbo.HinhAnh",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DuongDan = c.String(maxLength: 1, fixedLength: true),
                        HinhAnh = c.String(maxLength: 1, fixedLength: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SanPhamKem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenSP = c.String(nullable: false, maxLength: 255),
                        MoTa = c.String(maxLength: 255),
                        DonViTinh = c.String(maxLength: 255),
                        Gia = c.Decimal(nullable: false, precision: 18, scale: 0),
                        IDHinh = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HinhAnh", t => t.IDHinh)
                .Index(t => t.IDHinh);
            
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HoTen = c.String(maxLength: 255),
                        SDT = c.Int(),
                        Loai = c.String(nullable: false, maxLength: 10),
                        GhiChu = c.String(maxLength: 255),
                        Username = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LichHen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ThoiGian = c.DateTime(nullable: false, storeType: "date"),
                        TenKH = c.String(nullable: false, maxLength: 255),
                        SDT = c.Int(nullable: false),
                        ThoiGianHen = c.DateTime(nullable: false, storeType: "date"),
                        TenThoCat = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPhamKem", "IDHinh", "dbo.HinhAnh");
            DropForeignKey("dbo.DichVu", "IDHinh", "dbo.HinhAnh");
            DropForeignKey("dbo.DangNhap", "IDNV", "dbo.NhanVien");
            DropForeignKey("dbo.CongNhanVien", "IDNV", "dbo.NhanVien");
            DropForeignKey("dbo.NhanVien", "ChucVuID", "dbo.ChucVu");
            DropForeignKey("dbo.ChiTietHoaDon", "IDHoaDon", "dbo.HoaDon");
            DropIndex("dbo.SanPhamKem", new[] { "IDHinh" });
            DropIndex("dbo.DichVu", new[] { "IDHinh" });
            DropIndex("dbo.DangNhap", new[] { "IDNV" });
            DropIndex("dbo.CongNhanVien", new[] { "IDNV" });
            DropIndex("dbo.NhanVien", new[] { "ChucVuID" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "IDHoaDon" });
            DropTable("dbo.LichHen");
            DropTable("dbo.KhachHang");
            DropTable("dbo.SanPhamKem");
            DropTable("dbo.HinhAnh");
            DropTable("dbo.DichVu");
            DropTable("dbo.DangNhap");
            DropTable("dbo.CongNhanVien");
            DropTable("dbo.NhanVien");
            DropTable("dbo.ChucVu");
            DropTable("dbo.HoaDon");
            DropTable("dbo.ChiTietHoaDon");
        }
    }
}
