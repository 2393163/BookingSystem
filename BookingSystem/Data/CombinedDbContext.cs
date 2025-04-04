using Microsoft.EntityFrameworkCore;
using BookingSystem.Entities;

namespace BookingSystem.Data
{
    public class CombinedDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TravelBookingDb-2;Integrated Security=True;TrustServerCertificate=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Assistance> Assistances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints here
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Package)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PackageID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.User)
                .WithMany(u => u.Insurances)
                .HasForeignKey(i => i.UserID)
                .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<Insurance>()
            //    .HasOne(i => i.Booking)
            //    .WithMany(b => b.Insurances)
            //    .HasForeignKey(i => i.BookingID)
            //    .OnDelete(DeleteBehavior.Cascade);






            modelBuilder.Entity<Assistance>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assistances)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Add other configurations as needed
        }
    }
}
