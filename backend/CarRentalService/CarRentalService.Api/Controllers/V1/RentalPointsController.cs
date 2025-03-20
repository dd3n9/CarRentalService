using CarRentalService.Application.RentalPoints.Commands.Create;
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


        [HttpPost]
        [Authorize(Roles = StaticUserRoles.MANAGER)]
        public async Task<IActionResult> CreateRentalPoint([FromBody] CreateRentalPointRequest request)
        {
            var command = new CreateRentalPointCommand(request.Name, request.City, request.Street);
            var result = await _mediator.Send(command);

            return OkOrNotFound(result);
        }
    }
}
