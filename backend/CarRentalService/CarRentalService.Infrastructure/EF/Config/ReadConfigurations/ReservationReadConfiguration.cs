using CarRentalService.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.EF.Config.ReadConfigurations
{
    internal sealed class ReservationReadConfiguration : IEntityTypeConfiguration<ReservationReadModel>
    {
        public void Configure(EntityTypeBuilder<ReservationReadModel> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(rp => rp.Id);

            builder.HasOne(r => r.Vehicle)
                .WithMany(v => v.Reservations)
                .HasForeignKey(r => r.VehicleId);

            builder.HasOne(r => r.PickupPoint)
                .WithMany()
                .HasForeignKey(r => r.PickupPointId);

            builder.HasOne(r => r.ReturnPoint)
               .WithMany()
               .HasForeignKey(r => r.ReturnPointId);
        }
    }
}
