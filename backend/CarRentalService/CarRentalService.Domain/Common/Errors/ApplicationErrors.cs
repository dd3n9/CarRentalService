using FluentResults;

namespace CarRentalService.Domain.Common.Errors
{
    public class ApplicationErrors
    {
        public static class ApplicationUser
        {
            public static readonly Error NotFound = new Error("User was not found.")
                .WithMetadata("ErrorCode", "ApplicationUser.NotFound");
            public static readonly Error AlreadyExistsByEmail = new Error("Email is already taken.")
                .WithMetadata("ErrorCode", "ApplicationUser.AlreadyExistsByEmail");
            public static Error CustomValidationError(string message) => new Error(message)
                .WithMetadata("ErrorCode", "ApplicationUser.Validation");
        }

        public static class Vehicle
        {
            public static readonly Error NotFound = new Error("Vehicle was not found.")
                .WithMetadata("ErrorCode", "Vehicle.NotFound");
            public static readonly Error VehicleNotAvailableForSelectedTime = new Error("Vehicle is not available for this time slot.")
                .WithMetadata("ErrorCode", "Vehicle.NotAvailableForSelectedTime");
            public static readonly Error RentalPointLocationError = new Error("Vehicle is not located at the specified pickup point.")
                .WithMetadata("ErrorCode", "Vehicle.RentalPointLocationError");
        }

        public static class RentalPoint
        {
            public static readonly Error NotFound = new Error("Rental point was not found.")
                .WithMetadata("ErrorCode", "RentalPoint.NotFound");
        }
    }
}
