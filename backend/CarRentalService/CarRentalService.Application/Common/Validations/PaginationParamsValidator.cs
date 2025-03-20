using CarRentalService.Contracts.Common;
using FluentValidation;

namespace CarRentalService.Application.Common.Validations
{
    internal sealed class PaginationParamsValidator : AbstractValidator<PaginationParams>
    {
        public PaginationParamsValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(p => p.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(30).WithMessage("Page size cannot exceed 30.");
        }
    }
}
