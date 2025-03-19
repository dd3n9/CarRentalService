using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.User
{
    public class EmptyRefreshTokenIdException : CustomException
    {
        public EmptyRefreshTokenIdException()
            : base("RefreshTokenId cannot be empty", StatusCodes.Status400BadRequest)
        {

        }
    }
}
