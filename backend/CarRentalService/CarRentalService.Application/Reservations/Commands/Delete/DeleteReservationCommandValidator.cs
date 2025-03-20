using FluentValidation;

namespace CarRentalService.Application.Reservations.Commands.Delete
{
    public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
    {
        public DeleteReservationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty");

            RuleFor(x => x.ReservationId)
                .NotEqual(Guid.Empty).WithMessage("Reservation ID cannot be empty");
        }
    }
}
