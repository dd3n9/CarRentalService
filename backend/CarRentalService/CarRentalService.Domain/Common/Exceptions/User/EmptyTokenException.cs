using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.User
{
    public class EmptyTokenException : CustomException
    {
        public EmptyTokenException()
            : base("Token in RefreshToken cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}
