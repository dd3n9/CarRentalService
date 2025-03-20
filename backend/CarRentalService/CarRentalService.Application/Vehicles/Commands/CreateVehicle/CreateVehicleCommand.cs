using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Commands.CreateVehicle
{
    public record CreateVehicleCommand(
        string VehicleBrand,
        string VehicleModel,
        decimal PricePerDay,
        string VehicleType,
        string LicensePlate,
        int VehicleYear,
        int VehicleSeats,
        Guid RentalPointId) : IRequest<Result>;
}
