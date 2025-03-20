using FluentResults;
using MediatR;

namespace CarRentalService.Application.RentalPoints.Commands.Create
{
    public record CreateRentalPointCommand(
        string Name,
        string City,
        string Street ) : IRequest<Result>;
}
