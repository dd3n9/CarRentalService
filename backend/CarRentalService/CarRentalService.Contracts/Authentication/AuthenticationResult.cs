using CarRentalService.Contracts.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Contracts.Authentication
{
    public record AuthenticationResult(AuthenticationDto AuthenticationDto, string Token, string RefreshToken);
}
