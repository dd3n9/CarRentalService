using FluentValidation;

namespace CarRentalService.Application.Vehicles.Commands.ReturnVehicle
{
    public class ReturnVehicleCommandValidator : AbstractValidator<ReturnVehicleCommand>
    {
        public ReturnVehicleCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty");

            RuleFor(x => x.ReservationId)
                .NotEqual(Guid.Empty).WithMessage("Reservation ID cannot be empty");
        }
    }
}