using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.RentalPoint
{
    public class EmptyRentalPointNameException : CustomException
    {
        public EmptyRentalPointNameException()
            : base("Rental Point Name cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}