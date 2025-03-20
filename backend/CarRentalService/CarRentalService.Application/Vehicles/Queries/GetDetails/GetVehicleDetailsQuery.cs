using CarRentalService.Contracts.Vehicles;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Queries.GetDetails
{
    public record GetVehicleDetailsQuery(Guid VehicleId) : IRequest<Result<VehicleDetailsResponse>>;
}
