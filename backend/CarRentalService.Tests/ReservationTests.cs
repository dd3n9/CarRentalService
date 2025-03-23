using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Common.Exceptions.Vehicle;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate.Entities;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Tests
{
    public class ReservationTests
    {
        [Fact]
        public void Create_ShouldInitializeReservation_WithCorrectProperties()
        {
            // Arrange
            var userId = UserId.CreateUnique();
            var vehicleId = VehicleId.CreateUnique();
            var pickupPointId = RentalPointId.CreateUnique();
            var returnPointId = RentalPointId.CreateUnique();
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(2);

            // Act
            var reservation = Reservation.Create(userId, vehicleId, pickupPointId, returnPointId, startDate, endDate);

            // Assert
            reservation.Should().NotBeNull();
            reservation.UserId.Should().Be(userId);
            reservation.VehicleId.Should().Be(vehicleId);
            reservation.PickupPointId.Should().Be(pickupPointId);
            reservation.ReturnPointId.Should().Be(returnPointId);
            reservation.StartDate.Value.Should().Be(startDate);
            reservation.EndDate.Value.Should().Be(endDate);
            reservation.Status.Should().Be(ReservationStatus.Active);
            reservation.ReturnedDate.Should().BeNull();
        }

        [Fact]
        public void Create_WhenStartDateInPast_ShouldThrowException()
        {
            // Arrange
            var userId = UserId.CreateUnique();
            var vehicleId = VehicleId.CreateUnique();
            var pickupPointId = RentalPointId.CreateUnique();
            var returnPointId = RentalPointId.CreateUnique();
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Action act = () => Reservation.Create(userId, vehicleId, pickupPointId, returnPointId, startDate, endDate);
            act.Should().Throw<InvalidReservationDateException>();
        }

        [Fact]
        public void Complete_WhenActive_ShouldSucceed()
        {
            // Arrange
            var reservation = CreateSampleReservation();
            var returnDate = new ReservationDate(DateTime.UtcNow.AddDays(2));

            // Act
            var result = reservation.Complete(returnDate);

            // Assert
            result.IsSuccess.Should().BeTrue();
            reservation.Status.Should().Be(ReservationStatus.Completed);
            reservation.ReturnedDate.Should().Be(returnDate);
        }

        [Fact]
        public void Complete_WhenNotActive_ShouldFail()
        {
            // Arrange
            var reservation = CreateSampleReservation();
            reservation.Complete(new ReservationDate(DateTime.UtcNow.AddDays(2))); 
            var newReturnDate = new ReservationDate(DateTime.UtcNow.AddDays(3));

            // Act
            var result = reservation.Complete(newReturnDate);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        private Reservation CreateSampleReservation()
        {
            return Reservation.Create(
                UserId.CreateUnique(),
                VehicleId.CreateUnique(),
                RentalPointId.CreateUnique(),
                RentalPointId.CreateUnique(),
                DateTime.UtcNow.AddDays(1),
                DateTime.UtcNow.AddDays(2));
        }
    }
}
