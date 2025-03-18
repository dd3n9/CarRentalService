using CarRentalService.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.EF.Config.ReadConfigurations
{
    internal sealed class RentalPointReadConfiguration : IEntityTypeConfiguration<RentalPointReadModel>
    {
        public void Configure(EntityTypeBuilder<RentalPointReadModel> builder)
        {
            builder.HasKey(rp => rp.Id);
        }
    }
}
