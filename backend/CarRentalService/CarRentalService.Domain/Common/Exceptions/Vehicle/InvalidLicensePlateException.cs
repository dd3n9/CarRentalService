using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class InvalidLicensePlateException : CustomException
    {
        public InvalidLicensePlateException()
            : base("The license plate format is incorrect.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
