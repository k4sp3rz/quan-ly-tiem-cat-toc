namespace hottoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string HoTen { get; set; }

        public int? SDT { get; set; }

        [Required]
        [StringLength(10)]
        public string Loai { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }
    }
}
