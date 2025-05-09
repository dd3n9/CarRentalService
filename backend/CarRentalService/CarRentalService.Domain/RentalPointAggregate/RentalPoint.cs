﻿using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;

namespace CarRentalService.Domain.RentalPointAggregate
{
    public class RentalPoint : AggregateRoot<RentalPointId>
    {
        public RentalPointName Name { get; private set; }
        public RentalPointAddress Address { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;


        private RentalPoint() { } 

        private RentalPoint(
            RentalPointId rentalPointId,
            RentalPointName name,
            RentalPointAddress address ) : base(rentalPointId)
        {
            Name = name;
            Address = address;
        }

        public static RentalPoint Create(
            RentalPointName name,
            string city, 
            string street)
        {
            var rentalPoint = new RentalPoint(
                RentalPointId.CreateUnique(),
                name,
                RentalPointAddress.Create(city, street));

            return rentalPoint;
        }
    }
}
