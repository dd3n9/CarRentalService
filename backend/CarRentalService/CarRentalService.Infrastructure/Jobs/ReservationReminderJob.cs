using CarRentalService.Application.Common.Clients;
using CarRentalService.Domain.VehicleAggregate;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Models;
using CarRentalService.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace CarRentalService.Infrastructure.Jobs
{
    internal class ReservationReminderJob : IJob
    {
        private readonly DbSet<VehicleReadModel> _vehicles;
        private readonly IHubContext<ReservationHub, IReservationClient> _hubContext;

        public ReservationReminderJob(ReadDbContext readDbContext,
             IHubContext<ReservationHub, IReservationClient> hubContext)
        {
            _vehicles = readDbContext.Vehicles;
            _hubContext = hubContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.UtcNow.AddMinutes(30);
            var normalizedTime = new DateTime(
                now.Year, now.Month, now.Day,
                now.Hour, now.Minute, 0, DateTimeKind.Utc);

            var upcomingReturns = await _vehicles
                .SelectMany(v => v.Reservations)
                .Include(r => r.Vehicle)
                .Where(r =>
                    r.EndDate >= normalizedTime &&
                    r.EndDate < normalizedTime.AddMinutes(1))
                .ToListAsync();

            foreach (var reservation in upcomingReturns)
            {
                var userId = reservation.UserId.ToString();
                var message = $"Your vehicle {reservation.Vehicle.Brand} {reservation.Vehicle.Model} must be returned by {reservation.EndDate:HH:mm} on {reservation.EndDate:dd/MM/yyyy}.";

                await _hubContext.Clients.User(userId).ReceiveNotification(message);
            }
        }
    }
}
