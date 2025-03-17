using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CarRentalService.Domain.Common.Exceptions.Aggregate
{
    public class EmptyAggregateRootIdException : CustomException
    {
        public EmptyAggregateRootIdException()
            : base("Application User Id cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}
