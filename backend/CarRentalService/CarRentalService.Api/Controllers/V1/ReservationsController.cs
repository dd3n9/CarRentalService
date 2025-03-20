using CarRentalService.Api.Extensions;
using CarRentalService.Application.Reservations.Commands.Create;
using CarRentalService.Application.Reservations.Commands.Delete;
using CarRentalService.Application.Reservations.Queries;
using CarRentalService.Contracts.Common;
using CarRentalService.Contracts.Reservations.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ReservationsController : BaseController
    {
        private readonly ISender _mediator;

        public ReservationsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var command = new CreateReservationCommand(
                userId,
                request.VehicleId,
                request.PickupPointId,
                request.ReturnPointId,
                request.StartDate,
                request.EndDate);

            var result = await _mediator.Send(command);

            return OkOrNotFound(result);
        }

        [HttpGet("my-reservations")]
        public async Task<IActionResult> GetUserReservations([FromQuery] int pageSize, [FromQuery] int pageNumber, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var paginationParams = new PaginationParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var query = new GetUserReservationsQuery(userId, paginationParams);
            var result = await _mediator.Send(query);

            return OkOrNotFound(result);
        }


        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid reservationId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var command = new DeleteReservationCommand(userId, reservationId);
            var result = await _mediator.Send(command, cancellationToken);

            return OkOrNotFound(result);
        }
    }
}
