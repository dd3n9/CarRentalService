using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class InvalidPriceException : CustomException
    {
        public InvalidPriceException()
            : base("The price should be greater than 0.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
