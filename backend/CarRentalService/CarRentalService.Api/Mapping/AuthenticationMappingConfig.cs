using CarRentalService.Application.Authentication.Commands.Register;
using CarRentalService.Application.Authentication.Queries.Login;
using CarRentalService.Contracts.Authentication;
using Mapster;

namespace CarRentalService.Api.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();
        }
    }
}
