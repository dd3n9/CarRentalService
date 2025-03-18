using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace CarRentalService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken);
        Task<Result> AddAsync(User applicationUser, CancellationToken cancellationToken);
        void Update(User applicationUser, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
