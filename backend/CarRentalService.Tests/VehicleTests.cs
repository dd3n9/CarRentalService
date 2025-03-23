using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using FluentAssertions;

namespace CarRentalService.Tests
{
    public class VehicleTests
    {
        [Fact]
        public void Create_ShouldInitializeVehicle_WithCorrectProperties()
        {
            // Arrange
            var brand = new VehicleBrand("Toyota");
            var model = new VehicleModel("Camry");
            var price = new Price(50m);
            var type = VehicleType.Car;
            var licensePlate = new LicensePlate("AB1234CD");
            var year = new VehicleYear(2020);
            var seats = new VehicleSeats(5);
            var rentalPointId = RentalPointId.CreateUnique();

            // Act
            var vehicle = Vehicle.Create(brand, model, price, type, licensePlate, year, seats, rentalPointId);

            // Assert
            vehicle.Should().NotBeNull();
            vehicle.Brand.Should().Be(brand);
            vehicle.Model.Should().Be(model);
            vehicle.PricePerDay.Should().Be(price);
            vehicle.Type.Should().Be(type);
            vehicle.LicensePlate.Should().Be(licensePlate);
            vehicle.Year.Should().Be(year);
            vehicle.Seats.Should().Be(seats);
            vehicle.IsAvailable.Should().BeTrue();
            vehicle.RentalPointId.Should().Be(rentalPointId);
            vehicle.Reservations.Should().BeEmpty();
        }

        [Fact]
        public void Reserve_WhenAvailableAndNoOverlap_ShouldSucceed()
        {
            // Arrange
            var vehicle = CreateSampleVehicle();
            var userId = UserId.CreateUnique();
            var pickupPointId = vehicle.RentalPointId;
            var returnPointId = RentalPointId.CreateUnique();
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = vehicle.Reserve(userId, pickupPointId, returnPointId, startDate, endDate);

            // Assert
            result.IsSuccess.Should().BeTrue();
            vehicle.Reservations.Should().HaveCount(1);
            var reservation = result.Value;
            reservation.UserId.Should().Be(userId);
            reservation.VehicleId.Should().Be(vehicle.Id);
            reservation.StartDate.Value.Should().Be(startDate);
            reservation.EndDate.Value.Should().Be(endDate);
        }

        [Fact]
        public void Reserve_WhenNotAvailable_ShouldFail()
        {
            // Arrange
            var vehicle = CreateSampleVehicle();
            vehicle.MakeUnAvailable();
            var userId = UserId.CreateUnique();
            var pickupPointId = vehicle.RentalPointId;
            var returnPointId = RentalPointId.CreateUnique();
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = vehicle.Reserve(userId, pickupPointId, returnPointId, startDate, endDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            vehicle.Reservations.Should().BeEmpty();
        }

        [Fact]
        public void Reserve_WhenOverlappingReservationExists_ShouldFail()
        {
            // Arrange
            var vehicle = CreateSampleVehicle();
            var userId = UserId.CreateUnique();
            var pickupPointId = vehicle.RentalPointId;
            var returnPointId = RentalPointId.CreateUnique();
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(2);
            vehicle.Reserve(userId, pickupPointId, returnPointId, startDate, endDate);

            // Act
            var result = vehicle.Reserve(userId, pickupPointId, returnPointId, startDate.AddHours(1), endDate.AddHours(1));

            // Assert
            result.IsSuccess.Should().BeFalse();
            vehicle.Reservations.Should().HaveCount(1);
        }

        [Fact]
        public void RemoveReservation_WhenValid_ShouldSucceed()
        {
            // Arrange
            var vehicle = CreateSampleVehicle();
            var userId = UserId.CreateUnique();
            var pickupPointId = vehicle.RentalPointId;
            var returnPointId = RentalPointId.CreateUnique();
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(2);
            var reservationResult = vehicle.Reserve(userId, pickupPointId, returnPointId, startDate, endDate);

            // Act
            var result = vehicle.RemoveReservation(userId, reservationResult.Value.Id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            vehicle.Reservations.Should().BeEmpty();
        }

        [Fact]
        public void RemoveReservation_WhenReservationNotFound_ShouldFail()
        {
            // Arrange
            var vehicle = CreateSampleVehicle();
            var userId = UserId.CreateUnique();
            var reservationId = ReservationId.CreateUnique();

            // Act
            var result = vehicle.RemoveReservation(userId, reservationId);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        private Vehicle CreateSampleVehicle()
        {
            return Vehicle.Create(
                new VehicleBrand("Toyota"),
                new VehicleModel("Camry"),
                new Price(50m),
                VehicleType.Car,
                new LicensePlate("AB1234CD"),
                new VehicleYear(2020),
                new VehicleSeats(5),
                RentalPointId.CreateUnique());
        }
    }
}
