using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Infrastructure.EF.Context;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _user;
        private readonly WriteDbContext _writeDbContext;
        private readonly UserManager<User> _userManager;

        public UserRepository(WriteDbContext writeContext,
            UserManager<User> userManager)
        {
            _user = writeContext.Users;
            _writeDbContext = writeContext;
            _userManager = userManager;
        }

        public async Task<Result> AddAsync(User applicationUser, string password, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(applicationUser, password);
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => ApplicationErrors.ApplicationUser.CustomValidationError(e.Description))
                    .ToList();

                return Result.Fail(errors);
            }

            return Result.Ok();
        }

        public async Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken)
        {
            return await _user.SingleOrDefaultAsync(u => u.Id == id);
        }

        public void Update(User applicationUser, CancellationToken cancellationToken)
        {
            _user.Update(applicationUser);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
