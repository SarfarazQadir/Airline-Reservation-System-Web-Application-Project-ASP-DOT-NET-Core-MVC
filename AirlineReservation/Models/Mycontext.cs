using Microsoft.EntityFrameworkCore;

namespace AirlineReservation.Models
{
    public class Mycontext : DbContext
    {
        public Mycontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin> tbl_admin { get; set; }
        // DbSets for each model
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<FlightSchedule> FlightSchedules { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<DiscountOffer> DiscountOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // If needed, configure relationships, primary keys, and constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();  // Ensure unique email for users

            modelBuilder.Entity<Reservation>()
                .HasOne<User>()
                .WithMany()   // Assuming a user can have multiple reservations
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne<Flight>()
                .WithMany()   // Assuming a flight can have multiple reservations
                .HasForeignKey(r => r.FlightId);

            modelBuilder.Entity<Cancellation>()
                .HasOne<Reservation>()
                .WithMany()   // Assuming each reservation can be cancelled only once
                .HasForeignKey(c => c.ReservationId);

            modelBuilder.Entity<TicketStatus>()
                .HasOne<Reservation>()
                .WithMany()   // Each reservation may have multiple statuses over time
                .HasForeignKey(ts => ts.ReservationId);

            modelBuilder.Entity<FlightSchedule>()
                .HasOne<Flight>()
                .WithMany()   // Assuming one flight has multiple schedules
                .HasForeignKey(fs => fs.FlightId);

            modelBuilder.Entity<DiscountOffer>()
                .HasOne<Flight>()
                .WithMany()   // A flight can have multiple discount offers
                .HasForeignKey(d => d.FlightId);

        }
    }
}