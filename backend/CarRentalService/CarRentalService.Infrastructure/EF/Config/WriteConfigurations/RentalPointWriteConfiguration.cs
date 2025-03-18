using CarRentalService.Domain.RentalPointAggregate;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.EF.Config.WriteConfigurations
{
    public class RentalPointWriteConfiguration : IEntityTypeConfiguration<RentalPoint>
    {
        public void Configure(EntityTypeBuilder<RentalPoint> builder)
        {
            builder.HasKey(rp => rp.Id);

            builder.Property(rp => rp.Id)
                .HasConversion(rp => rp.Value, rp => new RentalPointId(rp));

            builder.Property(rp => rp.Name)
                .HasConversion(value => value.Value, value => new RentalPointName(value))
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(rp => rp.Address)
                .HasConversion(value => value.Value, value => new RentalPointAddress(value))
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();
        }
    }
}
