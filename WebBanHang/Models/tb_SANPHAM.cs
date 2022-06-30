namespace WebBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_SANPHAM()
        {
            tb_DANHGIA = new HashSet<tb_DANHGIA>();
            tb_GIOHANGCHITIET = new HashSet<tb_GIOHANGCHITIET>();
        }

        [Key]
        public int IDSANPHAM { get; set; }

        [Required]
        [StringLength(256)]
        public string TENANPHAM { get; set; }

        [Required]
        [StringLength(256)]
        public string HANG { get; set; }

        public double GIABAN { get; set; }

        [StringLength(256)]
        public string HINHANH { get; set; }

        [Column(TypeName = "ntext")]
        public string MOTA { get; set; }

        public bool TRANGTHAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_DANHGIA> tb_DANHGIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_GIOHANGCHITIET> tb_GIOHANGCHITIET { get; set; }
    }
}
