using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class InvalidVehicleSeatsCountException : CustomException
    {
        public InvalidVehicleSeatsCountException()
            : base("The number of seats should be from 1 to 9.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
