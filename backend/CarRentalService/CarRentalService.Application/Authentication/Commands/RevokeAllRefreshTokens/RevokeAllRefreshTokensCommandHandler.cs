using CarRentalService.Application.Common.Interfaces.Services;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Authentication.Commands.RevokeAllRefreshTokens
{
    public class RevokeAllRefreshTokensCommandHandler : IRequestHandler<RevokeAllRefreshTokensCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService;

        public RevokeAllRefreshTokensCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(RevokeAllRefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RevokeAllRefreshTokensAsync(request.UserId, cancellationToken);

            return result;
        }
    }
}
