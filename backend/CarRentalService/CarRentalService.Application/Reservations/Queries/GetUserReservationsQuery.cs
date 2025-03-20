using CarRentalService.Contracts.Common;
using CarRentalService.Contracts.Reservations.Responses;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Reservations.Queries
{
    public record GetUserReservationsQuery(string UserId, PaginationParams PaginationParams) :
        IRequest<Result<PaginatedList<ReservationResponse>>>;
}
