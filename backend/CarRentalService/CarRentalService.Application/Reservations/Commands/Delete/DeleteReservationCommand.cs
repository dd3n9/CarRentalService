using FluentResults;
using MediatR;

namespace CarRentalService.Application.Reservations.Commands.Delete
{
    public record DeleteReservationCommand(Guid ReservationId) : IRequest<Result>;
}
