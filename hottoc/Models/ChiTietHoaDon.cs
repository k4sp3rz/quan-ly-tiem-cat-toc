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
        public int ID { get; set; }

        public int? IDHoaDon { get; set; }

        [StringLength(255)]
        public string TenSP { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
