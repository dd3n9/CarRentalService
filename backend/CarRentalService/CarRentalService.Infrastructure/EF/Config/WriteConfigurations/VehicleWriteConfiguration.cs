using CarRentalService.Domain.RentalPointAggregate;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.EF.Config.WriteConfigurations
{
    public class VehicleWriteConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(v => v.Value, v => new VehicleId(v));

            builder.Property(v => v.Brand)
                .HasConversion(value => value.Value, value => new VehicleBrand(value))
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Model)
                .HasConversion(value => value.Value, value => new VehicleModel(value))
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.PricePerDay)
               .HasConversion(value => value.Value, value => new Price(value))
               .IsRequired();

            builder.Property(r => r.Type)
                .HasConversion(
                    status => status.ToString(),
                    value => (VehicleType)Enum.Parse(typeof(VehicleType), value)
                );

            builder.Property(v => v.LicensePlate)
                .HasConversion(value => value.Value, value => new LicensePlate(value))
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(v => v.Year)
                .HasConversion(value => value.Value, value => new VehicleYear(value))
                .IsRequired();

            builder.Property(v => v.Seats)
                .HasConversion(value => value.Value, value => new VehicleSeats(value))
                .IsRequired();

            builder.Property(v => v.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(v => v.RentalPointId)
                .IsRequired()
                .HasConversion(rp => rp.Value, rp => new RentalPointId(rp));

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.OwnsMany(v => v.Reservations, r =>
            {
                r.ToTable("Reservations");

                r.WithOwner()
                    .HasForeignKey(r => r.VehicleId);

                r.HasKey(r => r.Id);

                r.Property(r => r.Id)
                    .HasConversion(r => r.Value, r => new ReservationId(r));

                r.HasOne<RentalPoint>()
                    .WithMany()
                    .HasForeignKey(r => r.ReturnPointId)
                    .OnDelete(DeleteBehavior.Restrict);

                r.HasOne<RentalPoint>()
                    .WithMany()
                    .HasForeignKey(r => r.PickupPointId)
                    .OnDelete(DeleteBehavior.Restrict);

                r.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(r => r.UserId);

                r.Property(r => r.UserId)
                    .IsRequired()
                    .HasConversion(u => u.Value, u => new UserId(u));

                r.Property(r => r.VehicleId)
                    .IsRequired()
                    .HasConversion(v => v.Value, v => new VehicleId(v));

                r.Property(r => r.PickupPointId)
                    .IsRequired()
                    .HasConversion(rp => rp.Value, rp => new RentalPointId(rp));

                r.Property(r => r.ReturnPointId)
                    .IsRequired()
                    .HasConversion(rp => rp.Value, rp => new RentalPointId(rp));

                r.Property(r => r.StartDate)
                    .HasConversion(sd => sd.Value, sd => new ReservationDate(sd))
                    .IsRequired();

                r.Property(r => r.EndDate)
                    .HasConversion(ed => ed.Value, ed => new ReservationDate(ed))
                    .IsRequired();

                r.Property(r => r.ReturnedDate)
                    .HasConversion(rd => rd.Value, rd => new ReservationDate(rd));

                r.Property(r => r.Status)
                    .HasConversion(
                        status => status.ToString(),
                        value => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), value)
                    );

                r.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();
            });

            //Seed Data
            SeedVehicles(builder);
        }


        private void SeedVehicles(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasData(
                new
                {
                    Id = VehicleId.CreateUnique(),
                    Brand = new VehicleBrand("Toyota"),
                    Model = new VehicleModel("Camry"),
                    PricePerDay = new Price(50m),
                    Type = VehicleType.Car,
                    LicensePlate = new LicensePlate("KR1234AB"),
                    Year = new VehicleYear(2020),
                    Seats = new VehicleSeats(5),
                    IsAvailable = true,
                    RentalPointId = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440100"))
                },
                 new
                 {
                     Id = VehicleId.CreateUnique(),
                     Brand = new VehicleBrand("Toyota"),
                     Model = new VehicleModel("Supra"),
                     PricePerDay = new Price(100m),
                     Type = VehicleType.Car,
                     LicensePlate = new LicensePlate("KR7777AB"),
                     Year = new VehicleYear(2020),
                     Seats = new VehicleSeats(2),
                     IsAvailable = true,
                     RentalPointId = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440100"))
                 },
                new
                {
                    Id = VehicleId.CreateUnique(),
                    Brand = new VehicleBrand("Honda"),
                    Model = new VehicleModel("Civic"),
                    PricePerDay = new Price(45m),
                    Type = VehicleType.Car,
                    LicensePlate = new LicensePlate("WA5678CD"),
                    Year = new VehicleYear(2021),
                    Seats = new VehicleSeats(5),
                    IsAvailable = true,
                    RentalPointId = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440100"))
                },
                new
                {
                    Id = VehicleId.CreateUnique(),
                    Brand = new VehicleBrand("Ford"),
                    Model = new VehicleModel("Focus"),
                    PricePerDay = new Price(40m),
                    Type = VehicleType.Car,
                    LicensePlate = new LicensePlate("PO9012EF"),
                    Year = new VehicleYear(2019),
                    Seats = new VehicleSeats(4),
                    IsAvailable = true,
                    RentalPointId = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440101"))
                },
                 new
                 {
                     Id = VehicleId.CreateUnique(),
                     Brand = new VehicleBrand("Ford"),
                     Model = new VehicleModel("F-150"),
                     PricePerDay = new Price(80m),
                     Type = VehicleType.Truck,
                     LicensePlate = new LicensePlate("PO4012FF"),
                     Year = new VehicleYear(2018),
                     Seats = new VehicleSeats(2),
                     IsAvailable = true,
                     RentalPointId = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440101"))
                 },
                  new
                  {
                      Id = VehicleId.CreateUnique(),
                      Brand = new VehicleBrand("Yamaha"),
                      Model = new VehicleModel("MT-07"),
                      PricePerDay = new Price(35m),
                      Type = VehicleType.Motorcycle,
                      LicensePlate = new LicensePlate("PO9014EL"),
                      Year = new VehicleYear(2021),
                      Seats = new VehicleSeats(2),
                      IsAvailable = true,
                      RentalPointId = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440101"))
                  }
            );
        }
    }
}
