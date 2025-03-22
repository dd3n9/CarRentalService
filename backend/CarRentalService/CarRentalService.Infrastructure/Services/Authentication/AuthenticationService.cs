using CarRentalService.Application.Common.Interfaces.Services;
using CarRentalService.Contracts.Common.DTOs;
using CarRentalService.Contracts.Configurations;
using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.UserAggregate;
using CarRentalService.Domain.UserAggregate.Entities;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using FluentResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalService.Infrastructure.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleService _userRoleService;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, 
            IUserRepository userRepository,
            IOptions<JwtConfig> jwtConfig, 
            IUserRoleService userRoleService)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _jwtConfig = jwtConfig.Value;
            _userRoleService = userRoleService;
        }

        public async Task<Result<AuthTokensDto>> GenerateJwtTokenAsync(AuthenticationDto authenticationDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(authenticationDto.UserId, cancellationToken);

            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            var authResult = await GenerateTokensForUserAsync(user);
            _userRepository.Update(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok(authResult);
        }

        public async Task<Result<AuthTokensDto>> RefreshTokenAsync(string accessToken, Token refreshToken, CancellationToken cancellationToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal is null)
                return Result.Fail(ApplicationErrors.Authentication.InvalidToken);

            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null || string.IsNullOrWhiteSpace(userIdClaim.Value))
                return Result.Fail(ApplicationErrors.Authentication.InvalidToken);

            var user = await _userRepository.GetByIdAsync(userIdClaim.Value, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            var storedRefreshToken = user.FindRefreshToken(refreshToken);
            if (storedRefreshToken is null || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
                return Result.Fail(ApplicationErrors.Authentication.RefreshTokenExpired);

            user.RemoveRefreshToken(storedRefreshToken);

            var newAuthResult = await GenerateTokensForUserAsync(user);
            _userRepository.Update(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok(newAuthResult);
        }

        private string ExtractJtiFromToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(Token token)
        {
            var secret = _jwtConfig.Secret;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token.Value, tokenValidationParameters, out _);
        }

        private async Task<AuthTokensDto> GenerateTokensForUserAsync(User user)
        {
            var userRoles = await _userRoleService.GetUserRolesAsync(user.Id);
            var authenticationDto = new AuthenticationDto(user.Id, user.FirstName, user.LastName, userRoles);
            var jwtTokenValue = await _jwtTokenGenerator.GenerateToken(authenticationDto);
            var refreshToken = RefreshToken.Create(ExtractJtiFromToken(jwtTokenValue),
                DateTime.UtcNow,
                DateTime.UtcNow.AddMonths(_jwtConfig.RefreshTokenExpiryMonths));

            user.AddRefreshToken(refreshToken);

            return new AuthTokensDto(jwtTokenValue, refreshToken.Token);
        }

        public async Task<Result> RevokeAllRefreshTokensAsync(UserId applicationUserId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(applicationUserId, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            user.RevokeAllRefreshTokens();

            _userRepository.Update(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
