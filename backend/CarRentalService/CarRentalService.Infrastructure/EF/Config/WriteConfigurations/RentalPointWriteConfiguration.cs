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

            //Seed Data
            SeedRentalPoints(builder);
        }

        private void SeedRentalPoints(EntityTypeBuilder<RentalPoint> builder)
        {
            builder.HasData(
                new
                {
                    Id = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440100")),
                    Name = new RentalPointName("Warsaw Central"),
                    Address = new RentalPointAddress("Warsaw, Main St 1")
                },
                new
                {
                    Id = new RentalPointId(Guid.Parse("550e8400-e29b-41d4-a716-446655440101")),
                    Name = new RentalPointName("Katowice Station"),
                    Address = new RentalPointAddress("Katowice, Central Ave 10")
                }
            );
        }
    }
}
