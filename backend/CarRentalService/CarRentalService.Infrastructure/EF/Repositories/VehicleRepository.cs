using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using CarRentalService.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.EF.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DbSet<Vehicle> _vehicles;
        private readonly WriteDbContext _writeDbContext;

        public VehicleRepository(WriteDbContext writeDbContext)
        {
            _vehicles = writeDbContext.Vehicles;
            _writeDbContext = writeDbContext;
        }

        public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            await _vehicles.AddAsync(vehicle, cancellationToken);
        }

        public async Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken)
        {
            return await _vehicles
                .Include(v => v.Reservations)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public void Update(Vehicle vehicle)
        {
            if (vehicle is null)
                throw new ArgumentNullException(nameof(vehicle));

            _vehicles.Update(vehicle);
        }

        public void Delete(Vehicle vehicle)
        {
            if (vehicle is null)
                throw new ArgumentNullException(nameof(vehicle));

            _vehicles.Remove(vehicle);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Vehicle?> GetByReservationIdAsync(ReservationId reservationId, CancellationToken cancellationToken)
        {
            return await _vehicles
                .Include(v => v.Reservations)
                .SingleOrDefaultAsync(v => v.Reservations.Any(r => r.Id == reservationId), cancellationToken);  
        }
    }
}
