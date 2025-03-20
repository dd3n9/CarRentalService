using FluentValidation;

namespace CarRentalService.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleId)
                .NotEqual(Guid.Empty).WithMessage("Vehicle ID cannot be empty");
        }
    }
}
