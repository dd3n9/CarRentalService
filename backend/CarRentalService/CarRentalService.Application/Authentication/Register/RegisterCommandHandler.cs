using FluentResults;
using MediatR;

namespace CarRentalService.Application.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {

        public RegisterCommandHandler()
        {
            
        }

        public Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
