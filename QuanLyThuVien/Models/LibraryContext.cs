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
        public DbSet<CardReader> CardReaders { get; set; }
        public DbSet<ListBorrowed> ListBorrowed { get; set; }
        public DbSet<Employee> Employees { get; set; }

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
            modelBuilder.Entity<CardReader>()
                .HasOne(cr => cr.Reader)
                .WithMany(r => r.CardReaders)
                .HasForeignKey(cr => cr.IdReader)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListBorrowed>()
                .HasOne(lb => lb.CardReader)
                .WithMany(cr => cr.ListBorrowed)
                .HasForeignKey(lb => lb.IdCard)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListBorrowed>()
                .HasOne(lb => lb.Book)
                .WithMany(b => b.ListBorrowed)
                .HasForeignKey(lb => lb.IdBook)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
