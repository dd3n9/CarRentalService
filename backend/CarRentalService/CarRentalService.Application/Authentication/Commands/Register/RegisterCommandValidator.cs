using FluentValidation;

namespace CarRentalService.Application.Authentication.Commands.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 64).WithMessage("First name must be between 2 and 64 characters.")
                .Matches(@"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ']+$").WithMessage("First name can only contain letters and apostrophes.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 64).WithMessage("Last name must be between 2 and 64 characters.")
                .Matches(@"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ']+$").WithMessage("Last name can only contain letters and apostrophes.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
            .Matches(@"[\!\?\*\.]").WithMessage("Password must contain at least one special character (!? * .)");
        }
    }
}
