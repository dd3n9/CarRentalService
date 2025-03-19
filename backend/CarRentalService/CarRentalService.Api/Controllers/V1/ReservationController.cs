using CarRentalService.Api.Extensions;
using CarRentalService.Application.Reservations.Commands.Create;
using CarRentalService.Application.Reservations.Commands.Delete;
using CarRentalService.Contracts.Reservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ReservationController : BaseController
    {
        private readonly ISender _mediator;

        public ReservationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
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

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid reservationId, CancellationToken cancellationToken)
        {
            var command = new DeleteReservationCommand(reservationId);
            var result = await _mediator.Send(command, cancellationToken);

            return OkOrNotFound(result);
        }
    }
}
