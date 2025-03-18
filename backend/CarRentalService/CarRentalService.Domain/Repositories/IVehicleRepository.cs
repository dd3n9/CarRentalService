using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;

namespace CarRentalService.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken);
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
        void Update(Vehicle vehicle, CancellationToken cancellationToken);
        void Delete(Vehicle vehicle, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
