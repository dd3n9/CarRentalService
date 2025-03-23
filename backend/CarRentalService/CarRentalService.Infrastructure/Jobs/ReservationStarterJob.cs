using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Domain.VehicleAggregate.ValueObjects;
using CarRentalService.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace CarRentalService.Infrastructure.Jobs
{
    internal class ReservationStarterJob : IJob
    {
        private readonly DbSet<Vehicle> _vehicles;
        private readonly WriteDbContext _dbContext;

        public ReservationStarterJob(WriteDbContext writeDbContext)
        {
            _vehicles = writeDbContext.Vehicles;
            _dbContext = writeDbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.UtcNow;
            var normalizedTime = new DateTime(
                now.Year, now.Month, now.Day,
                now.Hour, now.Minute, 0, DateTimeKind.Utc);

            var upcomingVehiclesReservations = await _vehicles
                .Include(v => v.Reservations)
                 .Where(v => v.IsAvailable == true && v.Reservations.Any(r =>
                    r.Status == ReservationStatus.Active &&
                    r.StartDate >= normalizedTime &&
                    r.StartDate < normalizedTime.AddMinutes(1)))
                .ToListAsync();

            foreach (var vehicle in upcomingVehiclesReservations)
            {
                if (vehicle.IsAvailable)
                {
                    vehicle.MakeUnAvailable();
                }
            }

            if (upcomingVehiclesReservations.Any())
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
