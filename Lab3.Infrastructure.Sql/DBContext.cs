using Microsoft.EntityFrameworkCore;
using Lab3.Infrastructure.Sql.Models;
using Microsoft.Extensions.Configuration;

namespace Lab3.Infrastructure.Sql
{
    public partial class DBContext : DbContext
    {
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<BooksOrders> BooksOrders { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Sages> Sages { get; set; }
        public virtual DbSet<SagesBooks> SagesBooks { get; set; }
        public virtual DbSet<UserCart> UserCart { get; set; }

        public DBContext(DbContextOptions options)
            : base(options)
        {
         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-OHJUKI3;Database=Lab3DB;Trusted_Connection=True;MultipleActiveResultSets=true");    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BooksOrders>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.BookId });

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BooksOrders)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToBooksOrdersFromBooks");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.BooksOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToBooksOrdersFromOrders");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.IsCompleted).HasColumnName("isCompleted");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Sages>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Photo).HasColumnType("binary(50)");
            });

            modelBuilder.Entity<SagesBooks>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.SageId });

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.SagesBooks)
                    .HasForeignKey(d => new { d.BookId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToSagesBooksFromBooks");

                entity.HasOne(d => d.Sage)
                    .WithMany(p => p.SagesBooks)
                    .HasForeignKey(d => new { d.SageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToSagesBooksFromSages");
            });

            modelBuilder.Entity<UserCart>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BookId });

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.UserCart)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToSageCartFromBooks");
            });
        }
    }
}
