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
            var now = DateTime.UtcNow;
            var reminderWindowStart = now; // Поточний час
            var reminderWindowEnd = now.AddMinutes(30); // Поточний час + 30 хвилин

            var upcomingReturns = await _vehicles
                .SelectMany(v => v.Reservations)
                .Include(r => r.Vehicle)
                .Where(r =>
                    r.Status == "Active" && // Перевіряємо лише активні резервації
                    r.EndDate >= reminderWindowStart && // EndDate >= поточного часу
                    r.EndDate <= reminderWindowEnd) // EndDate <= поточного часу + 30 хвилин
                .ToListAsync();


            foreach (var reservation in upcomingReturns)
            {
                var userId = reservation.UserId.ToString();
                var message = $"Your vehicle {reservation.Vehicle.Brand} must be returned by {reservation.EndDate:HH:mm}.";

                await _hubContext.Clients.User(userId).ReceiveNotification(message);
            }

        }
    }
}
