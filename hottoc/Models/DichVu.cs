namespace hottoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DichVu")]
    public partial class DichVu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string TenDV { get; set; }

        public decimal Gia { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        public int? IDHinh { get; set; }

        public virtual HinhAnh HinhAnh { get; set; }
    }
}
