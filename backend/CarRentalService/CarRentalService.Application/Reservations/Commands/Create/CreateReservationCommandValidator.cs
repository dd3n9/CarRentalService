using FluentValidation;

namespace CarRentalService.Application.Reservations.Commands.Create
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty");

            RuleFor(x => x.VehicleId)
                .NotEqual(Guid.Empty).WithMessage("Vehicle ID cannot be empty");

            RuleFor(x => x.PickupPointId)
                .NotEqual(Guid.Empty).WithMessage("Pickup Point ID cannot be empty");

            RuleFor(x => x.ReturnPointId)
                .NotEqual(Guid.Empty).WithMessage("Return Point ID cannot be empty");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date cannot be empty")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the past");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date cannot be empty")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date");
        }
    }
}
