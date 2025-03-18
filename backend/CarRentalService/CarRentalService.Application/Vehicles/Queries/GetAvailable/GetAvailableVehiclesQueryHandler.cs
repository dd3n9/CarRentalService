using CarRentalService.Application.Common.Helpers;
using CarRentalService.Application.Common.Interfaces.ReadServices;
using CarRentalService.Contracts.Common;
using CarRentalService.Contracts.Common.DTOs;
using CarRentalService.Contracts.Vehicles;
using FluentResults;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Application.Vehicles.Queries.GetAvailable
{
    public class GetAvailableVehiclesQueryHandler : IRequestHandler<GetAvailableVehiclesQuery, Result<PaginatedList<VehicleResponse>>>
    {
        private readonly IVehicleReadService _vehicleReadService;

        public GetAvailableVehiclesQueryHandler(IVehicleReadService vehicleReadService)
        {
            _vehicleReadService = vehicleReadService;
        }

        public async Task<Result<PaginatedList<VehicleResponse>>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            var query = _vehicleReadService.GetAvailableVehiclesQuery();

            if (!string.IsNullOrWhiteSpace(request.City))
            {
                query = query.Where(v => EF.Functions.Like(v.RentalCity, $"%{request.City}%"));
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                query = query.Where(v => !v.Reservations.Any(r =>
                    r.StartDate < request.EndDate &&
                    r.EndDate > request.StartDate));
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

            var vehicles = await CollectionHelper<VehicleDto>.ToPaginatedList(query, request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
            var response = vehicles.Adapt<PaginatedList<VehicleResponse>>();

            return Result.Ok(response);
        }
    }
}
