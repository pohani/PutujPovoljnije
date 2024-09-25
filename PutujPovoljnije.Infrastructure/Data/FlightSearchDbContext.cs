using Microsoft.EntityFrameworkCore;
using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Infrastructure.Data
{
    public class FlightSearchDbContext : DbContext
    {
        public DbSet<FlightSearch> FlightSearches { get; set; }
        public DbSet<FlightOffer> FlightOffers { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Airport> Airports { get; set; }

        public FlightSearchDbContext(DbContextOptions<FlightSearchDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightOffer>()
                .HasOne(f => f.FlightSearch)
                .WithMany(fs => fs.FlightOffers)
                .HasForeignKey(f => f.FlightSearchId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }
    }
}
