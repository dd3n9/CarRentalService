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
                .HasConversion(value => value.Value, value => new UserId(value))
                .ValueGeneratedNever();

            builder
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.OwnsMany(u => u.RefreshTokens, rtb =>
            {
                rtb.HasKey(u => u.Id);

                rtb.Property(u => u.Id)
                    .HasConversion(id => id.Value, id => new RefreshTokenId(id))
                    .ValueGeneratedNever();

                rtb.WithOwner()
                    .HasForeignKey("ApplicationUserId");

                rtb.Property(u => u.Token)
                    .HasConversion(t => t.Value, t => new Token(t));

                rtb.Property(u => u.JwtId)
                    .HasConversion(jwtId => jwtId.Value, jwtId => new JwtId(jwtId));

                rtb.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                rtb.Property(r => r.AddedDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();

                rtb.Property(r => r.ExpiryDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();
            });
        }
    }
}
