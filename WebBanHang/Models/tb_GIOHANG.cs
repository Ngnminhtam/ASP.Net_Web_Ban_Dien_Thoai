namespace WebBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_GIOHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_GIOHANG()
        {
            tb_GIOHANGCHITIET = new HashSet<tb_GIOHANGCHITIET>();
        }

        [Key]
        public int IDGIOHANG { get; set; }

        [Required]
        [StringLength(128)]
        public string Id { get; set; }

        public DateTime NGAYDAT { get; set; }

        public DateTime NGAYGIAO { get; set; }

        [StringLength(1)]
        public string GHICHU { get; set; }

        public double? THANHTIEN { get; set; }

        public bool THANHTOAN { get; set; }

        [Required]
        [StringLength(32)]
        public string TRANGTHAI { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_GIOHANGCHITIET> tb_GIOHANGCHITIET { get; set; }
    }
}
