using CarRentalService.Contracts.Common;
using CarRentalService.Contracts.Vehicles;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Queries.GetAvailable
{
    public record GetAvailableVehiclesQuery(
        string? City,
        DateTime? StartDate,
        DateTime? EndDate,
        int? YearFrom,
        int? YearTo,
        string? SearchTerm,
        string? SortByPrice, 
        PaginationParams PaginationParams) : IRequest<Result<PaginatedList<VehicleResponse>>>;
}
