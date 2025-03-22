using CarRentalService.Application.Common.Interfaces.Services;
using CarRentalService.Contracts.Authentication;
using CarRentalService.Contracts.Common.DTOs;
using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.UserAggregate;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarRentalService.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;


        public LoginQueryHandler(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IAuthenticationService authenticationService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);


            var isPasswordCorrect = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!isPasswordCorrect.Succeeded)
                return Result.Fail(ApplicationErrors.Authentication.IncorrectPassword);

            var roles = await _userManager.GetRolesAsync(user);

            var authDto = new AuthenticationDto(user.Id, user.FirstName, user.LastName, roles);

            var token = await _authenticationService.GenerateJwtTokenAsync(authDto, cancellationToken);
            if (token.IsFailed)
                return Result.Fail(token.Errors);

            var result = new AuthenticationResult(authDto, token.Value.Token, token.Value.RefreshToken);

            return Result.Ok(result);
        }
    }
}
