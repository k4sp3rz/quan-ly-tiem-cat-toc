namespace hottoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichHen")]
    public partial class LichHen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ThoiGian { get; set; }

        [Required]
        [StringLength(255)]
        public string TenKH { get; set; }

        public int SDT { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ThoiGianHen { get; set; }

        [StringLength(255)]
        public string TenThoCat { get; set; }
    }
}
