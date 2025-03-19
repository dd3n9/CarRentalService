using Microsoft.EntityFrameworkCore;
using CarRentalService.Infrastructure.EF.Models;
using CarRentalService.Infrastructure.EF.Config.ReadConfigurations;

namespace CarRentalService.Infrastructure.EF.Context
{
    internal sealed class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<VehicleReadModel> Vehicles { get; set; }
        public DbSet<ReservationReadModel> Reservations { get; set; }
        public DbSet<RentalPointReadModel> RentalPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            WriteConfiguration(modelBuilder);
        }

        private void WriteConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleReadConfiguration());
            modelBuilder.ApplyConfiguration(new UserReadConfiguration());
            modelBuilder.ApplyConfiguration(new RentalPointReadConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationReadConfiguration());
        }
    }
}
