using FluentValidation;

namespace CarRentalService.Application.RentalPoints.Commands.Delete
{
    public class DeleteRentalPointCommandValidator : AbstractValidator<DeleteRentalPointCommand>
    {
        public DeleteRentalPointCommandValidator()
        {
            RuleFor(x => x.RentalPointId)
                .NotEqual(Guid.Empty).WithMessage("Rental Point ID cannot be empty");
        }
    }
}
