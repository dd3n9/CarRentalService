using FluentValidation;

namespace CarRentalService.Application.Authentication.Commands.RevokeAllRefreshTokens
{
    public sealed class RevokeAllRefreshTokensCommandValidator : AbstractValidator<RevokeAllRefreshTokensCommand>
    {
        public RevokeAllRefreshTokensCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .NotNull().WithMessage("User ID cannot be null.")
                .NotEqual(string.Empty).WithMessage("User ID cannot be an empty string.");
        }
    }
}
