using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.VehicleAggregate.Entities;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;

namespace CarRentalService.Domain.VehicleAggregate
{
    public class Vehicle : AggregateRoot<VehicleId>
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public VehicleType Type { get; private set; }
        public string LicensePlate { get; private set; }
        public int Year { get; private set; }
        public int Seats { get; private set; }
        public bool IsAvailable { get; private set; }

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
            IsAvailable = true;
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
