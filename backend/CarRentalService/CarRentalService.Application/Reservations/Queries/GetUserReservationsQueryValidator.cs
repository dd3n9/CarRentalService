using CarRentalService.Application.Common.Validations;
using FluentValidation;

namespace CarRentalService.Application.Reservations.Queries
{
    public class GetUserReservationsQueryValidator : AbstractValidator<GetUserReservationsQuery>
    {
        public GetUserReservationsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty");

            RuleFor(q => q.PaginationParams)
                .NotNull().WithMessage("Pagination parameters are required.")
                .SetValidator(new PaginationParamsValidator());
        }

    }
}
