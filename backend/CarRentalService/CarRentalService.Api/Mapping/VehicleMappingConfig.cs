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
        }
    }
}
