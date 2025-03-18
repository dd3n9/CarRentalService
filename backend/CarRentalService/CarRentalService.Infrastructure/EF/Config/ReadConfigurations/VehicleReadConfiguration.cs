using CarRentalService.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.EF.Config.ReadConfigurations
{
    internal sealed class VehicleReadConfiguration : IEntityTypeConfiguration<VehicleReadModel>
    {
        public void Configure(EntityTypeBuilder<VehicleReadModel> builder)
        {
            builder.HasKey(v => v.Id);

            builder.HasMany(v => v.Reservations)
                .WithOne(r => r.Vehicle)
                .HasForeignKey(r => r.VehicleId);
        }
    }
}
