using CarRentalService.Domain.RentalPointAggregate;
using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.Entities;
using CarRentalService.Infrastructure.EF.Config.WriteConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRentalService.Infrastructure.EF.Context
{
    public sealed class WriteDbContext : IdentityDbContext<User, IdentityRole<UserId>, UserId>
    {
        public WriteDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RentalPoint> RentalPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            WriteConfiguration(modelBuilder);
            UserIdentityConfiguration(modelBuilder);
        }

        private void WriteConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleWriteConfiguration());
            modelBuilder.ApplyConfiguration(new UserWriteConfiguration());
            modelBuilder.ApplyConfiguration(new RentalPointWriteConfiguration());
        }

        private void UserIdentityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
            });

            var guidToStringConverter = new ValueConverter<UserId, string>(
                id => id.Value,
                value => new UserId(value)
            );

            modelBuilder.Entity<IdentityUserClaim<UserId>>(builder =>
            {
                builder.Property(uc => uc.UserId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<UserId>>(builder =>
            {
                builder.Property(ul => ul.UserId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<UserId>>(builder =>
            {
                builder.Property(ut => ut.UserId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserTokens");
            });

            modelBuilder.Entity<IdentityRole<UserId>>(builder =>
            {
                builder.Property(r => r.Id)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityRoleClaim<UserId>>(builder =>
            {
                builder.Property(rc => rc.RoleId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<UserId>>(builder =>
            {
                builder.Property(ur => ur.UserId)
                       .HasConversion(guidToStringConverter);

                builder.Property(ur => ur.RoleId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserRoles");
            });
        }
    }
}
