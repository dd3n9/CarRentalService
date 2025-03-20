using FluentValidation;

namespace CarRentalService.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleBrand)
                .NotEmpty().WithMessage("Vehicle brand cannot be empty")
                .Length(2, 50).WithMessage("Vehicle brand must be between 2 and 50 characters")
                .Matches(@"^[a-zA-Zа-яА-ЯёЁіІїЇєЄґҐ\s-]+$").WithMessage("Vehicle brand can only contain letters, spaces, or hyphens");

            RuleFor(x => x.VehicleModel)
                .NotEmpty().WithMessage("Vehicle model cannot be empty")
                .Length(1, 50).WithMessage("Vehicle model must be between 1 and 50 characters")
                .Matches(@"^[a-zA-Zа-яА-ЯёЁіІїЇєЄґҐ0-9\s-]+$").WithMessage("Vehicle model can only contain letters, numbers, spaces, or hyphens");

            RuleFor(x => x.PricePerDay)
                .GreaterThan(0).WithMessage("Price per day must be greater than 0");

            RuleFor(x => x.VehicleType)
                .NotEmpty().WithMessage("Vehicle type cannot be empty");

            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("License plate cannot be empty")
                .Length(5, 15).WithMessage("License plate must be between 5 and 15 characters")
                .Matches(@"^[A-Z0-9\s-]+$").WithMessage("License plate can only contain uppercase letters, numbers, spaces, or hyphens");

            RuleFor(x => x.VehicleYear)
                .InclusiveBetween(1990, DateTime.Today.Year).WithMessage($"Vehicle year must be between 1900 and {DateTime.Today.Year}");

            RuleFor(x => x.VehicleSeats)
                .GreaterThan(0).WithMessage("Vehicle seats must be greater than 0")
                .LessThanOrEqualTo(50).WithMessage("Vehicle seats cannot exceed 50");

            RuleFor(x => x.RentalPointId)
                .NotEqual(Guid.Empty).WithMessage("Rental Point ID cannot be empty");
        }
    }
}
