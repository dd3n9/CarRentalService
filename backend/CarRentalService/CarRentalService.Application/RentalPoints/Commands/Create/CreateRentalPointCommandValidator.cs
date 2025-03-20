using FluentValidation;

namespace CarRentalService.Application.RentalPoints.Commands.Create
{
    public class CreateRentalPointCommandValidator : AbstractValidator<CreateRentalPointCommand>
    {
        public CreateRentalPointCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters")
                .Matches(@"^[a-zA-Zа-яА-ЯёЁіІїЇєЄґҐ0-9\s-]+$").WithMessage("Name can only contain letters, numbers, spaces, or hyphens");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City cannot be empty")
                .Length(2, 50).WithMessage("City must be between 2 and 50 characters")
                .Matches(@"^[a-zA-Zа-яА-ЯёЁіІїЇєЄґҐ\s-]+$").WithMessage("City can only contain letters, spaces, or hyphens");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street cannot be empty")
                .Length(2, 100).WithMessage("Street must be between 2 and 100 characters")
                .Matches(@"^[a-zA-Zа-яА-ЯёЁіІїЇєЄґҐ0-9\s-]+$").WithMessage("Street can only contain letters, numbers, spaces, or hyphens");
        }
    }
}
