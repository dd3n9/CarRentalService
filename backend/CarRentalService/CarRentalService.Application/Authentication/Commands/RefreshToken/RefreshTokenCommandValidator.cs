using FluentValidation;

namespace CarRentalService.Application.Authentication.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access token is required.")
                .MinimumLength(10).WithMessage("Access token must be at least 10 characters long.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.")
                .MinimumLength(32).WithMessage("Refresh token must be at least 32 characters long.");
        }
    }
}
