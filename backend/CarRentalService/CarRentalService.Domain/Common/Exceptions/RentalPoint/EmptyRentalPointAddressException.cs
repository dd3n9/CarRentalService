using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Domain.Common.Exceptions.RentalPoint
{
    public class EmptyRentalPointAddressException : CustomException
    {
        public EmptyRentalPointAddressException()
            : base("Rental Point Address cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
