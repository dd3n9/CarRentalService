using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DeleteVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);
            if (vehicle is null)
                return Result.Fail(ApplicationErrors.Vehicle.NotFound);

            if (vehicle.Reservations.Any(r => r.Status == ReservationStatus.Active))
                return Result.Fail(ApplicationErrors.Vehicle.CannotDeleteWithActiveReservations);

            _vehicleRepository.Delete(vehicle);
            await _vehicleRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
