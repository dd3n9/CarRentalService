using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.EF.Config.WriteConfigurations
{
    public class UserWriteConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Id)
                .HasConversion(value => value.Value, value => new UserId(value));

            builder
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();
        }
    }
}
