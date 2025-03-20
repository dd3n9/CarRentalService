using FluentValidation;

namespace CarRentalService.Application.Vehicles.Queries.GetDetails
{
    public class GetVehicleDetailsQueryValidator : AbstractValidator<GetVehicleDetailsQuery>
    {
        public GetVehicleDetailsQueryValidator()
        {
            RuleFor(q => q.VehicleId)
                .NotEmpty().WithMessage("Vehicle ID cannot be empty");
        }
    }
}
