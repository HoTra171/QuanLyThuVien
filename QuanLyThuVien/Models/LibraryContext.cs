using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ListBorrowed> ListBorrowed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["LibraryDatabase"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            // Tạo chỉ mục UNIQUE cho USER_ACCOUNT
            modelBuilder.Entity<AccountUser>()
                .HasIndex(au => au.UserAccount)
                .IsUnique();

            // Cấu hình quan hệ READER -> LIST_BORROWED
            modelBuilder.Entity<ListBorrowed>()
                .HasOne(lb => lb.Reader)
                .WithMany(r => r.ListBorrowed)
                .HasForeignKey(lb => lb.IdReader)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade khi Reader bị xóa

            // Cấu hình quan hệ BOOK -> LIST_BORROWED
            modelBuilder.Entity<ListBorrowed>()
                .HasOne(lb => lb.Book)
                .WithMany(b => b.ListBorrowed)
                .HasForeignKey(lb => lb.IdBook)
                .OnDelete(DeleteBehavior.SetNull); // Set NULL khi Book bị xóa
        }
    }
}
