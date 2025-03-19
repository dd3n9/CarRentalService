using CarRentalService.Application.Common.Interfaces.Services;
using CarRentalService.Contracts.Common.DTOs;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthTokensDto>>
    {
        private readonly IAuthenticationService _authenticatinService;

        public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticatinService = authenticationService;
        }

        public async Task<Result<AuthTokensDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authenticatinService.RefreshTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);
        }
    }
}
