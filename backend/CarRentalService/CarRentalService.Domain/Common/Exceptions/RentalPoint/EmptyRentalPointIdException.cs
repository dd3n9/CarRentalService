using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.RentalPoint
{
    public class EmptyRentalPointIdException : CustomException
    {
        public EmptyRentalPointIdException()
            : base("Rental Point Id cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
