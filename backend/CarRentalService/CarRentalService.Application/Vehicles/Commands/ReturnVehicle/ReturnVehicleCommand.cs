using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Commands.ReturnVehicle
{
    public record ReturnVehicleCommand(string UserId, Guid ReservationId) : IRequest<Result>;
}
