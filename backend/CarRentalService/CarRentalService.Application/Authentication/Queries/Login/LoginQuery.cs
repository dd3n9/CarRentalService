using CarRentalService.Contracts.Authentication;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Authentication.Queries.Login
{
    public record LoginQuery(
       string Email,
       string Password
       ) : IRequest<Result<AuthenticationResult>>;
}
