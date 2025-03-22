using FluentResults;
using MediatR;

namespace CarRentalService.Application.Reservations.Commands.Create
{
    public record CreateReservationCommand(
        string UserId, 
        Guid VehicleId,
        Guid ReturnPointId,
        DateTime StartDate,
        DateTime EndDate) : IRequest<Result>;
}
