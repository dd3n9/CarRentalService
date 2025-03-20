using CarRentalService.Application.Common.Helpers;
using CarRentalService.Application.Reservations.Queries;
using CarRentalService.Contracts.Common;
using CarRentalService.Contracts.Reservations.Responses;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Models;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Queries.Reservations
{
    internal class GetUserReservationsHandler : IRequestHandler<GetUserReservationsQuery, Result<PaginatedList<ReservationResponse>>>
    {
        private readonly DbSet<VehicleReadModel> _vehicles;

        public GetUserReservationsHandler(ReadDbContext readDbContext)
        {
            _vehicles = readDbContext.Vehicles;
        }

        public async Task<Result<PaginatedList<ReservationResponse>>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ReservationResponse> query = _vehicles
                .SelectMany(v => v.Reservations) 
                .Where(r => r.UserId == request.UserId)
                .Select(r => new ReservationResponse(
                    r.Id,
                    r.Vehicle.Brand,
                    r.Vehicle.Model,
                    r.StartDate,
                    r.EndDate,
                    r.Status,
                    r.CreatedAt
                ));

            var reservations = await CollectionHelper<ReservationResponse>.ToPaginatedList(query, request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
           

            return Result.Ok(new PaginatedList<ReservationResponse>(
                reservations.Items,
                reservations.TotalCount,
                reservations.CurrentPage,
                reservations.PageSize
            ));
        }
    }
}
