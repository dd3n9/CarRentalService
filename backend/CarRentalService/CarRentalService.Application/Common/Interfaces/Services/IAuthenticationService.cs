using CarRentalService.Contracts.Common.DTOs;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace CarRentalService.Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<Result<AuthTokensDto>> GenerateJwtTokenAsync(AuthenticationDto authenticationDto, CancellationToken cancellationToken);
        Task<Result<AuthTokensDto>> RefreshTokenAsync(string accessToken, Token refreshToken, CancellationToken cancellationToken);
        Task<Result> RevokeAllRefreshTokensAsync(UserId applicationUserId, CancellationToken cancellationToken);
    }
}
