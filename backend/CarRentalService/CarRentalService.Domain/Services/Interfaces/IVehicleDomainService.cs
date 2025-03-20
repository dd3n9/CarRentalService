using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using FluentResults;

namespace CarRentalService.Domain.Services.Interfaces
{
    public interface IVehicleDomainService
    {
        Task<Result> ReturnVehicleAsync(Vehicle vehicle, ReservationId reservationId, UserId userId, CancellationToken cancellationToken);
    }
}
