using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.User
{
    public class EmptyApplicationUserIdException : CustomException
    {
        public EmptyApplicationUserIdException()
            : base("Application User Id cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}
