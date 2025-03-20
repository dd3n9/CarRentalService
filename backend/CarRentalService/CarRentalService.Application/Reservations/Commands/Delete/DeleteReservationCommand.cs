using FluentResults;
using MediatR;

namespace CarRentalService.Application.Reservations.Commands.Delete
{
    public record DeleteReservationCommand(string UserId, Guid ReservationId) : IRequest<Result>;
}
