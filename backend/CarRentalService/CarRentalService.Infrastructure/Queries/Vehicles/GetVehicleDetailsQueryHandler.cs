using CarRentalService.Application.Vehicles.Queries.GetDetails;
using CarRentalService.Contracts.Vehicles;
using CarRentalService.Domain.Common.Errors;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Models;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Queries.Vehicles
{
    internal class GetVehicleDetailsQueryHandler : IRequestHandler<GetVehicleDetailsQuery, Result<VehicleDetailsResponse>>
    {
        private readonly DbSet<VehicleReadModel> _vehicles;

        public GetVehicleDetailsQueryHandler(ReadDbContext readDbContext)
        {
            _vehicles = readDbContext.Vehicles;
        }

        public async Task<Result<VehicleDetailsResponse>> Handle(GetVehicleDetailsQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicles
                .Include(v => v.RentalPoint)
                .Include(v => v.Reservations) 
                .FirstOrDefaultAsync(v => v.Id == request.VehicleId, cancellationToken);

            if (vehicle is null)
                return Result.Fail(ApplicationErrors.Vehicle.NotFound);

            var response = new VehicleDetailsResponse(
                vehicle.Id,
                vehicle.Brand,
                vehicle.Model,
                vehicle.Type.ToString(),
                vehicle.LicensePlate,
                vehicle.Year,
                vehicle.Seats,
                vehicle.PricePerDay,
                vehicle.IsAvailable,
                vehicle.RentalPoint.Name,
                vehicle.RentalPoint.Address);

            return Result.Ok(response);

        }
    }
}
