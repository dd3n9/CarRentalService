using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    internal class EmptyVehicleModelException : CustomException
    {
        public EmptyVehicleModelException()
            : base("Vehicle Model cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
