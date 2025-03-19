using CarRentalService.Application.Common.Interfaces.Services;
using CarRentalService.Contracts.Common.Constants;
using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace CarRentalService.Infrastructure.Services.Authentication
{
    internal class UserRoleService : IUserRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<UserId>> _roleManager;

        public UserRoleService(UserManager<User> userManager,
            RoleManager<IdentityRole<UserId>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleAsync(User applicationUser, string userRole)
        {
            if (await _userManager.IsInRoleAsync(applicationUser, userRole))
            {
                return;
            }

            await _userManager.AddToRoleAsync(applicationUser, userRole);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(UserId userId)
        {
            var user = await _userManager.FindByIdAsync(userId.Value);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<Result> SeedIdentityRoleDataAsync()
        {
            await SeedRolesAsync();
            await SeedMockDataAsync();

            return Result.Ok();
        }

        private async Task<Result> SeedRolesAsync()
        {
            bool isManagerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.MANAGER);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isManagerRoleExists && isUserRoleExists)
                return Result.Ok();

            await _roleManager.CreateAsync(new IdentityRole<UserId>
            {
                Id = UserId.CreateUnique(),
                Name = StaticUserRoles.MANAGER,
                NormalizedName = StaticUserRoles.MANAGER.ToUpper()
            });

            await _roleManager.CreateAsync(new IdentityRole<UserId>
            {
                Id = UserId.CreateUnique(),
                Name = StaticUserRoles.USER,
                NormalizedName = StaticUserRoles.USER.ToUpper()
            });

            return Result.Ok();
        }

        private async Task SeedMockDataAsync()
        {
            var managerEmail = "manager@example.com";
            if (await _userManager.FindByEmailAsync(managerEmail) == null)
            {
                var manager = User.Create("John", "Manager", managerEmail, "ManagerPass123!");
                var createResult = await _userManager.CreateAsync(manager, "ManagerPass123!");
                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(manager, StaticUserRoles.MANAGER);
                }
                else
                {
                    throw new Exception($"Failed to create mock manager: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
