using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Infrastructure.EF.Config.WriteConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.EF.Context
{
    public class WriteDbContext : IdentityDbContext<User, IdentityRole<UserId>, UserId>
    {
        public WriteDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            WriteConfiguration(modelBuilder);
        }

        private void WriteConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleWriteConfiguration());
            modelBuilder.ApplyConfiguration(new UserWriteConfiguration());
            modelBuilder.ApplyConfiguration(new RentalPointWriteConfiguration());
        }
    }
}
