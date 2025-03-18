using CarRentalService.Domain.RentalPointAggregate;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.Repositories;
using CarRentalService.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.EF.Repositories
{
    public class RentalPointRepository : IRentalPointRepository
    {
        private readonly DbSet<RentalPoint> _rentalPoint;
        private readonly WriteDbContext _writeDbContext;

        public RentalPointRepository(WriteDbContext writeDbContext)
        {
            _rentalPoint = writeDbContext.RentalPoints;
            _writeDbContext = writeDbContext;
        }

        public async Task AddAsync(RentalPoint rentalPoint, CancellationToken cancellationToken)
        {
            await _rentalPoint.AddAsync(rentalPoint, cancellationToken);
        }

        public async Task<RentalPoint?> GetByIdAsync(RentalPointId id, CancellationToken cancellationToken)
        {
            return await _rentalPoint
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public void Update(RentalPoint rentalPoint, CancellationToken cancellationToken)
        {
            if (rentalPoint is null)
                throw new ArgumentNullException(nameof(rentalPoint));

            _rentalPoint.Update(rentalPoint);
        }

        public void Delete(RentalPoint rentalPoint, CancellationToken cancellationToken)
        {
            if (rentalPoint is null)
                throw new ArgumentNullException(nameof(rentalPoint));

            _rentalPoint.Remove(rentalPoint);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
