using CarRentalService.Application.Common.Interfaces.ReadServices;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.EF.Services
{
    internal sealed class UserReadService : IUserReadService
    {
        private readonly DbSet<UserReadModel> _user;

        public UserReadService(ReadDbContext readDbContext)
        {
            _user = readDbContext.Users;
        }

        public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
             => _user.AnyAsync(u => u.Email == email, cancellationToken);
    }
}
