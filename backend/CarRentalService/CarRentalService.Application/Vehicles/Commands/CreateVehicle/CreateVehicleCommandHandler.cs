using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result>
    {
        private readonly IVehicleRepository _vehicleRepositor;
        private readonly IRentalPointRepository _rentalPointRepositor;

        public CreateVehicleCommandHandler(
            IVehicleRepository vehicleRepository, 
            IRentalPointRepository rentalPointRepository)
        {
            _vehicleRepositor = vehicleRepository;
            _rentalPointRepositor = rentalPointRepository;
        }

        public async Task<Result> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (await _vehicleRepositor.GetByLicensePlateAsync(request.LicensePlate, cancellationToken) is not null)
                return Result.Fail(ApplicationErrors.Vehicle.AlreadyExists);

            var rentalPoint = await _rentalPointRepositor.GetByIdAsync(request.RentalPointId, cancellationToken);
            if (rentalPoint is null)
                return Result.Fail(ApplicationErrors.RentalPoint.NotFound);

            if (!Enum.TryParse<VehicleType>(request.VehicleType, true, out var vehicleType))
            {
                return Result.Fail(ApplicationErrors.Vehicle.InvalidVehicleType);
            }

            var vehicle = Vehicle.Create(
                request.VehicleBrand,
                request.VehicleModel,
                request.PricePerDay,
                vehicleType,
                request.LicensePlate,
                request.VehicleYear,
                request.VehicleSeats,
                request.RentalPointId
                );

            await _vehicleRepositor.AddAsync(vehicle, cancellationToken);
            await _vehicleRepositor.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
