using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Vehicle
{
    public class EmptyReservationRentalPointIdException : CustomException
    {
        public EmptyReservationRentalPointIdException()
            : base("Reservation Rental Point Id cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
