using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.User
{
    public class EmptyJwtIdException : CustomException
    {
        public EmptyJwtIdException()
            : base("JwtId cannot be empty", StatusCodes.Status400BadRequest)
        {

        }
    }
}
