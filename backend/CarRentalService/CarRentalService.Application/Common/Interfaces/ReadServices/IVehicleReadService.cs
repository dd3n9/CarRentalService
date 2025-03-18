using CarRentalService.Contracts.Common.DTOs;

namespace CarRentalService.Application.Common.Interfaces.ReadServices
{
    public interface IVehicleReadService
    {
        IQueryable<VehicleDto> GetAvailableVehiclesQuery();
    }
}