using CarRentalService.Contracts.Common.DTOs;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<AuthTokensDto>>;
}
