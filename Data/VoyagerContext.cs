using NewVoyager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NewVoyager.Data
{
    public class VoyagerContext : IdentityDbContext<ApplicationUser>
    {
        public VoyagerContext(DbContextOptions<VoyagerContext> options) : base(options)
        {
        }

        public DbSet<Voyager> Voyagers { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Plans> Plans { get; set; }
        public DbSet<Trips> Trips { get; set; }


        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Voyager>().ToTable("Voyager");
            modelBuilder.Entity<Events>().ToTable("Event");
            modelBuilder.Entity<Plans>().ToTable("Plan");
            modelBuilder.Entity<Trips>().ToTable("Trip");

            modelBuilder.Entity<Plans>()
            .HasMany(p => p.Trips) // Define the relationship: Plans has many Trips
            .WithOne(t => t.Plan) // Each Trip has one Plan
            .HasForeignKey(t => t.PlanID) // Foreign key in Trips pointing to Plans
            .OnDelete(DeleteBehavior.Cascade); // Add cascade delete behavior

            modelBuilder.Entity<Trips>()
                .HasMany(t => t.Events) // Define the relationship: Trips has many Events
                .WithOne(e => e.Trip) // Each Event has one Trip
                .HasForeignKey(e => e.TripID) // Foreign key in Events pointing to Trips
                .OnDelete(DeleteBehavior.Cascade); // Add cascade delete behavior

            modelBuilder.Entity<Plans>()
                .HasOne(p => p.AppUser)
                .WithMany(v => v.Plans)
                .HasForeignKey(p => p.AppUserID);
           
        }
    }
}