using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.Services.Interfaces;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using FluentResults;

namespace CarRentalService.Domain.Services
{
    public class VehicleDomainService : IVehicleDomainService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleDomainService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result> ReturnVehicleAsync(Vehicle vehicle, ReservationId reservationId, UserId userId, CancellationToken cancellationToken)
        {
            var reservation = vehicle.Reservations.SingleOrDefault(r => r.Id == reservationId);
            if (reservation is null)
                return Result.Fail(ApplicationErrors.Reservation.NotFound);

            if (reservation.UserId != userId)
                return Result.Fail(ApplicationErrors.Reservation.AccessDenied);

            DateTime returnTime = DateTime.UtcNow;

            var completeResult = reservation.Complete(returnTime);
            if (completeResult.IsFailed)
                return completeResult;

            vehicle.MakeAvailable(reservation.ReturnPointId);

            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
