namespace WebBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_DANHGIA
    {
        [Key]
        public int IDDANHGIA { get; set; }

        [Required]
        [StringLength(128)]
        public string Id { get; set; }

        public int IDSANPHAM { get; set; }

        public double DIEM { get; set; }

        [Column(TypeName = "ntext")]
        public string DANHGIA { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual tb_SANPHAM tb_SANPHAM { get; set; }
    }
}
