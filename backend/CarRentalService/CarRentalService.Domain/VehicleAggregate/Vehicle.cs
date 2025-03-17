using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using CarRentalService.Domain.VehicleAggregate.Entities;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;

namespace CarRentalService.Domain.VehicleAggregate
{
    public class Vehicle : AggregateRoot<VehicleId>
    {
        public VehicleBrand Brand { get; private set; }
        public VehicleModel Model { get; private set; }
        public Price Price { get; private set; }
        public VehicleType Type { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public VehicleYear Year { get; private set; }
        public VehicleSeats Seats { get; private set; }
        public RentalPointId RentalPointId { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;


        private readonly List<Reservation> _reservations = new();
        public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();


        private Vehicle() { }

        private Vehicle(
            VehicleId vehicleId,
            string brand,
            string model,
            VehicleType type,
            string licensePlate,
            int year,
            int seats ) : base(vehicleId)
        {
            Brand = brand;
            Model = model;
            Type = type;
            LicensePlate = licensePlate;
            Year = year;
            Seats = seats;
        }

        public static Vehicle Create(
            string brand,
            string model,
            VehicleType type,
            string licensePlate,
            int year,
            int seats)
        {
            var vehicle = new Vehicle(
                    VehicleId.CreateUnique(),
                    brand,
                    model,
                    type,
                    licensePlate,
                    year,
                    seats);

            return vehicle;
        }
    }
}
