using CarRentalService.Application.RentalPoints.Commands.Create;
using CarRentalService.Application.RentalPoints.Commands.Delete;
using CarRentalService.Application.RentalPoints.Queries;
using CarRentalService.Contracts.Common.Constants;
using CarRentalService.Contracts.RentalPoints.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RentalPointsController : BaseController
    {
        private readonly ISender _mediator;

        public RentalPointsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("suggestions")]
        public async Task<IActionResult> GetRentalPointSuggestions([FromQuery] string prefix, CancellationToken cancellationToken)
        {
            var query = new GetRentalPointSuggestionsQuery(prefix);
            var result = await _mediator.Send(query, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpPost]
        [Authorize(Roles = StaticUserRoles.MANAGER)]
        public async Task<IActionResult> CreateRentalPoint([FromBody] CreateRentalPointRequest request)
        {
            var command = new CreateRentalPointCommand(request.Name, request.City, request.Street);
            var result = await _mediator.Send(command);

            return OkOrNotFound(result);
        }

        [HttpDelete]
        [Authorize(Roles = StaticUserRoles.MANAGER)]
        public async Task<IActionResult> DeleteRentalPoint([FromQuery] Guid rentalPointId)
        {
            var command = new DeleteRentalPointCommand(rentalPointId);
            var result = await _mediator.Send(command);

            return OkOrNotFound(result);
        }
    }
}
