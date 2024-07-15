namespace hottoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiTietHoaDon()
        {
        }
        public ChiTietHoaDon(int IDHoaDon, string TenSP, int SoLuong, decimal ThanhTien)
        {
            this.IDHoaDon = IDHoaDon;
            this.TenSP = TenSP;
            this.SoLuong = SoLuong;
            this.ThanhTien = ThanhTien;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IDHoaDon { get; set; }

        public string Loai { get; set; }

        [StringLength(255)]
        public string TenSP { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual HoaDon HoaDon { get; set; }

    }
}
