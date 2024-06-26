namespace hottoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string NhanVien { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Ngay { get; set; }

        public string TenKH { get; set; }

        public int? SDT { get; set; }

        public int? TongSLSP { get; set; }

        public int? TongTienSP { get; set; }

        public int TongSLDV { get; set; }

        [Required]
        public string TenDV { get; set; }

        public decimal ThanhTien { get; set; }

        public decimal KhachTra { get; set; }

        public decimal ThoiLai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
