using CarRentalService.Application.Common.Validations;
using FluentValidation;

namespace CarRentalService.Application.Vehicles.Queries.GetAvailable
{
    public class GetAvailableVehiclesQueryValidator : AbstractValidator<GetAvailableVehiclesQuery>
    {
        public GetAvailableVehiclesQueryValidator()
        {
            RuleFor(q => q.PaginationParams)
                .NotNull().WithMessage("Pagination parameters are required.")
                .SetValidator(new PaginationParamsValidator());
        }
    }
}
