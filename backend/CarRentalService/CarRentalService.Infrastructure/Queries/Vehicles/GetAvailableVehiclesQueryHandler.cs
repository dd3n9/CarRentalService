using CarRentalService.Application.Common.Helpers;
using CarRentalService.Application.Vehicles.Queries.GetAvailable;
using CarRentalService.Contracts.Common;
using CarRentalService.Contracts.Vehicles;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Models;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Queries.Vehicles
{
    internal class GetAvailableVehiclesQueryHandler : IRequestHandler<GetAvailableVehiclesQuery, Result<PaginatedList<VehicleResponse>>>
    {
        private readonly DbSet<VehicleReadModel> _vehicles;

        public GetAvailableVehiclesQueryHandler(ReadDbContext readDbContext)
        {
            _vehicles = readDbContext.Vehicles;
        }

        public async Task<Result<PaginatedList<VehicleResponse>>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<VehicleReadModel> query = _vehicles
                .Include(v => v.Reservations)
                .Include(v => v.RentalPoint);

            if (!string.IsNullOrWhiteSpace(request.City))
            {
                query = query.Where(v => v.RentalPoint.Address.Contains(request.City));
            }

            if (request.StartDate.HasValue || request.EndDate.HasValue)
            {
                query = query.Where(v => !v.Reservations.Any(r =>
                    (!request.StartDate.HasValue || r.EndDate > request.StartDate) &&
                    (!request.EndDate.HasValue || r.StartDate < request.EndDate)));
            }
            else
            {
                var currentTime = DateTime.UtcNow;

                query = query.Where(v => !v.Reservations.Any(r =>
                    r.StartDate <= currentTime &&
                    r.EndDate >= currentTime));
            }

            if (request.YearFrom.HasValue)
            {
                query = query.Where(v => v.Year >= request.YearFrom);
            }

            if (request.YearTo.HasValue)
            {
                query = query.Where(v => v.Year <= request.YearTo);
            }

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(v => v.Brand.ToLower().Contains(searchTerm) ||
                                         v.Model.ToLower().Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(request.SortByPrice))
            {
                query = request.SortByPrice.ToLower() switch
                {
                    "asc" => query.OrderBy(v => v.PricePerDay),
                    "desc" => query.OrderByDescending(v => v.PricePerDay),
                    _ => query
                };
            }

            var vehicles = await CollectionHelper<VehicleReadModel>.ToPaginatedList(query, request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
            var response = MapToVehicleResponse(vehicles);

            return Result.Ok(response);
        }

        private PaginatedList<VehicleResponse> MapToVehicleResponse(PaginatedList<VehicleReadModel> vehicles)
        {
            var vehicleResponses = vehicles.Items
                .Select(v => new VehicleResponse(
                    v.Id,  
                    v.Brand,
                    v.Model,
                    v.PricePerDay,
                    v.Year,
                    v.Seats
                ))
                .ToList();

            return new PaginatedList<VehicleResponse>(
                vehicleResponses,
                vehicles.TotalCount,
                vehicles.CurrentPage,
                vehicles.PageSize
            );
        }
    }
}
