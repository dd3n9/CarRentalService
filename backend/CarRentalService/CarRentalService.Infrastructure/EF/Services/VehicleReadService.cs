using CarRentalService.Application.Common.Interfaces.ReadServices;
using CarRentalService.Contracts.Common.DTOs;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.ModelExtensions;
using CarRentalService.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.EF.Services
{
    internal sealed class VehicleReadService : IVehicleReadService
    {
        private readonly DbSet<VehicleReadModel> _vehicles;

        public VehicleReadService(ReadDbContext readDbContext)
        {
            _vehicles = readDbContext.Vehicles;
        }

        public IQueryable<VehicleDto> GetAvailableVehiclesQuery()
        {
            return _vehicles
                 .Include(v => v.Reservations)
                 .Include(v => v.RentalPoint)
                 .Select(v => v.AsDto());
        }
    }
}
