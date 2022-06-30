using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebBanHang.Models
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
            : base("name=StoreContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tb_DANHGIA> tb_DANHGIA { get; set; }
        public virtual DbSet<tb_GIOHANG> tb_GIOHANG { get; set; }
        public virtual DbSet<tb_GIOHANGCHITIET> tb_GIOHANGCHITIET { get; set; }
        public virtual DbSet<tb_SANPHAM> tb_SANPHAM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRoles)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.tb_DANHGIA)
                .WithRequired(e => e.AspNetUsers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.tb_GIOHANG)
                .WithRequired(e => e.AspNetUsers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_GIOHANG>()
                .Property(e => e.GHICHU)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_GIOHANG>()
                .HasMany(e => e.tb_GIOHANGCHITIET)
                .WithRequired(e => e.tb_GIOHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SANPHAM>()
                .HasMany(e => e.tb_DANHGIA)
                .WithRequired(e => e.tb_SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SANPHAM>()
                .HasMany(e => e.tb_GIOHANGCHITIET)
                .WithRequired(e => e.tb_SANPHAM)
                .WillCascadeOnDelete(false);
        }
    }
}
