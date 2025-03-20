using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;

namespace CarRentalService.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken);
        Task<Vehicle?> GetByReservationIdAsync(ReservationId reservationId, CancellationToken cancellationToken);
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
