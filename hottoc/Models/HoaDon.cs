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
        public DateTime Ngay { get; set; } = DateTime.Now;

        public string TenKH { get; set; }

        public int? SDT { get; set; }

        [NotMapped]
        public List<int> DanhSachSP { get; set; }
        [NotMapped]
        public List<int> SoLuongSP { get; set; }

        public int? TongSLSP { get; set; }
        public int? TongTienSP { get; set; }

        [NotMapped]
        public List<int> DanhSachDV { get; set; }
        [NotMapped]
        public List<int> SoLuongDV { get; set; }

        public int? TongTienDV { get; set; }
        public int TongSLDV { get; set; }

        public decimal ThanhTien { get; set; }

        public decimal KhachTra { get; set; }

        public decimal ThoiLai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
