using CarRentalService.Contracts.Common.DTOs;

namespace CarRentalService.Application.Common.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(AuthenticationDto authenticationDto);
    }
}
