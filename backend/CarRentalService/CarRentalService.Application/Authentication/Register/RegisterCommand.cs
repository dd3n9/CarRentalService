using FluentResults;
using MediatR;
namespace CarRentalService.Application.Authentication.Register
{
    public record RegisterCommand(
       string FirstName,
       string LastName,
       string Email,
       string Password) : IRequest<Result>;
}
