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
            .HasOne(p => p.Voyager)
            .WithMany(v => v.Plans)
            .HasForeignKey(p => p.VoyagerID);
                
           
        }
    }
}