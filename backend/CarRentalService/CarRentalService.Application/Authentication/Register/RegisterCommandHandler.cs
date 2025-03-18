using CarRentalService.Application.Common.Interfaces.ReadServices;
using CarRentalService.Application.Common.Interfaces.Services;
using CarRentalService.Contracts.Common.Constants;
using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.UserAggregate;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IUserRoleService _userRoleService;

        public RegisterCommandHandler(IUserRepository userRepository,
            IUserRoleService userRoleService, 
            IUserReadService userReadService)
        {
            _userRepository = userRepository;
            _userRoleService = userRoleService;
            _userReadService = userReadService;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userReadService.ExistsByEmailAsync(request.Email, cancellationToken))
                return Result.Fail(ApplicationErrors.ApplicationUser.AlreadyExistsByEmail);

            var user = User.Create(request.FirstName, request.LastName, request.Email, request.Password);
            var createUserResult = await _userRepository.AddAsync(user, request.Password, cancellationToken);

            if (createUserResult.IsFailed)
            {
                return createUserResult;
            }

            await _userRoleService.AssignRoleAsync(user, StaticUserRoles.USER);

            return Result.Ok();
        }
    }
}
