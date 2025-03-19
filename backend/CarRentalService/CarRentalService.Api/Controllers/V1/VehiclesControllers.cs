using CarRentalService.Application.Vehicles.Queries.GetAvailable;
using CarRentalService.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class VehiclesControllers : BaseController
    {
        private readonly ISender _mediator;

        public VehiclesControllers(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableVehicles(
            [FromQuery] string? city,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? yearFrom,
            [FromQuery] int? yearTo,
            [FromQuery] string? searchTerm,
            [FromQuery] string? sortByPrice,
            [FromQuery] int pageSize,
            [FromQuery] int pageNumber)
        {
            var paginationParams = new PaginationParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var query = new GetAvailableVehiclesQuery(city, startDate, endDate, yearFrom, yearTo, searchTerm, sortByPrice, paginationParams);

            var result = await _mediator.Send(query);

            return OkOrNotFound(result);
        }
    }
}
