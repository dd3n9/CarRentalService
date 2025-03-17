using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class EmptyReservationIdException : CustomException
    {
        public EmptyReservationIdException()
            : base("Reservation ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
