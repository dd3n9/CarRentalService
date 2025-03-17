using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;
namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class EmptyVehicleIdException : CustomException
    {
        public EmptyVehicleIdException()
            : base("Vehicle ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
