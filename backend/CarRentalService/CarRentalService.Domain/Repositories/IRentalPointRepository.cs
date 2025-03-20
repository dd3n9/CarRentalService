using CarRentalService.Domain.RentalPointAggregate;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;

namespace CarRentalService.Domain.Repositories
{
    public interface IRentalPointRepository
    {
        Task<RentalPoint?> GetByIdAsync(RentalPointId id, CancellationToken cancellationToken);
        Task<RentalPoint?> GetByNameAsync(RentalPointName rentalPointName, CancellationToken cancellationToken);
        Task AddAsync(RentalPoint rentalPoint, CancellationToken cancellationToken);
        void Update(RentalPoint rentalPoint, CancellationToken cancellationToken);
        void Delete(RentalPoint rentalPoint, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
