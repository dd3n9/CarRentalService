using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate.Entities;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using FluentResults;

namespace CarRentalService.Domain.VehicleAggregate
{
    public class Vehicle : AggregateRoot<VehicleId>
    {
        public VehicleBrand Brand { get; private set; }
        public VehicleModel Model { get; private set; }
        public Price PricePerDay { get; private set; }
        public VehicleType Type { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public VehicleYear Year { get; private set; }
        public VehicleSeats Seats { get; private set; }
        public bool IsAvailable { get; private set; }
        public RentalPointId RentalPointId { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;


        private readonly List<Reservation> _reservations = new();
        public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();


        private Vehicle() { }

        private Vehicle(
            VehicleId vehicleId,
            VehicleBrand brand,
            VehicleModel model,
            Price pricePerDay,
            VehicleType type,
            LicensePlate licensePlate,
            VehicleYear year,
            VehicleSeats seats,
            RentalPointId rentalPointId) : base(vehicleId)
        {
            Brand = brand;
            Model = model;
            PricePerDay = pricePerDay;
            Type = type;
            LicensePlate = licensePlate;
            Year = year;
            Seats = seats;
            IsAvailable = true;
            RentalPointId = rentalPointId;
        }

        public static Vehicle Create(
            VehicleBrand brand,
            VehicleModel model,
            Price pricePerDay,
            VehicleType type,
            LicensePlate licensePlate,
            VehicleYear year,
            VehicleSeats seats,
            RentalPointId rentalPointId)
        {
            var vehicle = new Vehicle(
                    VehicleId.CreateUnique(),
                    brand,
                    model,
                    pricePerDay,
                    type,
                    licensePlate,
                    year,
                    seats,
                    rentalPointId
                    );

            return vehicle;
        }

        public Result<Reservation> Reserve(UserId userId, RentalPointId pickupPointId, RentalPointId returnPointId, DateTime startDate, DateTime endDate)
        {
            if (!IsAvailable || _reservations.Any(r => r.StartDate.Value < endDate && r.EndDate.Value > startDate))
                return Result.Fail(ApplicationErrors.Vehicle.VehicleNotAvailableForSelectedTime);

            if (RentalPointId != pickupPointId)
                return Result.Fail(ApplicationErrors.Vehicle.RentalPointLocationError);

            var reservation = Reservation.Create(userId, Id, pickupPointId, returnPointId, startDate, endDate);
            _reservations.Add(reservation);

            return Result.Ok(reservation);
        }

        public Result RemoveReservation(UserId userId, ReservationId reservationId)
        {
            var reservation = _reservations.SingleOrDefault(r => r.Id == reservationId);
            if (reservation is null)
                return Result.Fail(ApplicationErrors.Reservation.NotFound);

            if (reservation.UserId != userId)
                return Result.Fail(ApplicationErrors.Reservation.AccessDenied);

            if (reservation.StartDate < DateTime.UtcNow)
                return Result.Fail(ApplicationErrors.Reservation.EditableTimeExpired);

            _reservations.Remove(reservation);
            return Result.Ok();
        }

        public void MakeAvailable(RentalPointId returnPointId)
        {
            RentalPointId = returnPointId;
            IsAvailable = true;
        }
        public void MakeUnAvailable()
        {
            IsAvailable = false;
        }
    }
}
