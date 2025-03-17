using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class InvalidVehicleYearException : CustomException
    {
        public InvalidVehicleYearException()
            : base("The year must be in the range from 1900 to the current year.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
