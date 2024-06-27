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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int IDNV { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayCong { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
