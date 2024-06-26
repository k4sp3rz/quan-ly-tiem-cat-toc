namespace hottoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongNhanVien")]
    public partial class CongNhanVien
    {
        public int ID { get; set; }

        public int IDNV { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayCong { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
