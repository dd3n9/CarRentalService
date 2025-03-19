using FluentResults;
using MediatR;

namespace CarRentalService.Application.Authentication.Commands.RevokeAllRefreshTokens
{
    public record RevokeAllRefreshTokensCommand(string UserId) : IRequest<Result>;
}
