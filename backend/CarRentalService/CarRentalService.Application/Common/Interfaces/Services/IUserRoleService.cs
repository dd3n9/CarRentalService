using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace CarRentalService.Application.Common.Interfaces.Services
{
    public interface IUserRoleService
    {
        Task<Result> SeedRolesAsync();
        Task<IEnumerable<string>> GetUserRolesAsync(UserId userId);
        Task AssignRoleAsync(User applicationUser, string userRole);
    }
}
