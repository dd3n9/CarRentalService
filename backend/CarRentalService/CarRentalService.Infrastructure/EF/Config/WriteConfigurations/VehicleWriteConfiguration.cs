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

            builder.Property(v => v.Price)
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

            builder.Property(v => v.RentalPointId)
                .IsRequired()
                .HasConversion(rp => rp.Value, rp => new RentalPointId(rp));

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.OwnsMany(v => v.Reservations, r =>
            {
                r.ToTable("Reservation");

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
        }
    }
}
