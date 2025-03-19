using CarRentalService.Contracts.Common;
using CarRentalService.Contracts.Common.DTOs;
using CarRentalService.Contracts.Vehicles;
using Mapster;

namespace CarRentalService.Api.Mapping
{
    public class VehicleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<VehicleDto, VehicleResponse>()
                .Map(dest => dest.VehicleId, src => src.Id)
                .Map(dest => dest.Brand, src => src.Brand)
                .Map(dest => dest.Model, src => src.Model)
                .Map(dest => dest.PricePerDay, src => src.PricePerDay)
                .Map(dest => dest.Year, src => src.Year)
                .Map(dest => dest.Seats, src => src.Seats);

            config.NewConfig<PaginatedList<VehicleDto>, PaginatedList<VehicleResponse>>()
               .ConstructUsing(src => new PaginatedList<VehicleResponse>(
                   src.Items.Adapt<List<VehicleResponse>>(), 
                   src.TotalCount, 
                   src.CurrentPage,
                   src.PageSize))
               .Map(dest => dest.TotalCount, src => src.TotalCount)
               .Map(dest => dest.CurrentPage, src => src.CurrentPage)
               .Map(dest => dest.PageSize, src => src.PageSize);
        }
    }
}
