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

        public static class Reservation 
        {
            public static readonly Error NotFound = new Error("Reservation was not found.")
                .WithMetadata("ErrorCode", "Reservation.NotFound");
            public static readonly Error EditableTimeExpired = new Error("The reservation`s editable time period has expired.")
                .WithMetadata("ErrorCode", "Reservation.EditableTimeExpired");
        }


        public static class RentalPoint
        {
            public static readonly Error NotFound = new Error("Rental point was not found.")
                .WithMetadata("ErrorCode", "RentalPoint.NotFound");
        }

        public static class Authentication
        {
            public static readonly Error IncorrectPassword = new Error("Incorrect password.")
                .WithMetadata("ErrorCode", "Authentication.IncorrectPassword");
            public static readonly Error InvalidCredentials = new Error("Invalid email or password.")
                .WithMetadata("ErrorCode", "Authentication.InvalidCredentials");
            public static readonly Error InvalidToken = new Error("Invalid authentication token.")
                .WithMetadata("ErrorCode", "Authentication.InvalidToken");
            public static readonly Error ExpiredToken = new Error("Authentication token has expired.")
                 .WithMetadata("ErrorCode", "Authentication.ExpiredToken");
            public static readonly Error RefreshTokenNotFound = new Error("Refresh token not found.")
              .WithMetadata("ErrorCode", "Authentication.RefreshTokenNotFound");
            public static readonly Error RefreshTokenExpired = new Error("Refresh token has expired.")
             .WithMetadata("ErrorCode", "Authentication.RefreshTokenExpired");
        }
    }
}
