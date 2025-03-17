using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class EmptyVehicleBrandException : CustomException
    {
        public EmptyVehicleBrandException()
            : base("Vehicle Brand cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
