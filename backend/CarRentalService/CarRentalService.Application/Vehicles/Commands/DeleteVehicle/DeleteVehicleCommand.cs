using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand(Guid VehicleId) : IRequest<Result>;
}
