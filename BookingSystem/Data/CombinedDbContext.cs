using Microsoft.EntityFrameworkCore;
using BookingSystem.Entities;

namespace BookingSystem.Data
{
    public class CombinedDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TravelBookingDb-4;Integrated Security=True;TrustServerCertificate=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Assistance> AssistanceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Booking -> User
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking -> Package
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Package)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PackageID)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment -> Booking (1:1)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingID)
                .OnDelete(DeleteBehavior.Cascade);

            // Insurance -> User
            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.User)
                .WithMany(u => u.Insurances)
                .HasForeignKey(i => i.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Insurance -> Booking (many-to-one) with cascade delete
            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.Booking)
                .WithMany(b => b.Insurances)
                .HasForeignKey(i => i.BookingID)
                .OnDelete(DeleteBehavior.Cascade); // This enables cascade delete

            // Assistance -> User
            modelBuilder.Entity<Assistance>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assistances)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Default value for Package image
            modelBuilder.Entity<Package>()
                .Property(p => p.image)
                .HasDefaultValue("https://via.placeholder.com/150");
        }
    }
}
