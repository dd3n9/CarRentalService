using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class InvalidReservationDateException : CustomException
    {
        public InvalidReservationDateException()
            : base("Reservation date cannot be in the past.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
