namespace hottoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangNhap")]
    public partial class DangNhap
    {
        public int ID { get; set; }

        [StringLength(1)]
        public string Username { get; set; }

        [StringLength(1)]
        public string Password { get; set; }

        public int? IDNV { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
