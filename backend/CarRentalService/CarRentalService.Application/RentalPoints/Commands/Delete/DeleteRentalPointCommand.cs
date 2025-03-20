using FluentResults;
using MediatR;

namespace CarRentalService.Application.RentalPoints.Commands.Delete
{
    public record DeleteRentalPointCommand(Guid rentalPointId) : IRequest<Result>;
}
