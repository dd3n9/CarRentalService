using CarRentalService.Contracts.Common.DTOs;
using CarRentalService.Infrastructure.EF.Models;

namespace CarRentalService.Infrastructure.EF.ModelExtensions
{
    internal static class VehicleExtensions
    {
        public static VehicleDto AsDto(this VehicleReadModel model)
        => new(
            model.Id,
            model.Brand,
            model.Model,
            model.PricePerDay,
            model.Type,
            model.LicensePlate,
            model.Year,
            model.Seats,
            model.RentalPoint.Name,
            model.Reservations?.Select(r => r.AsDto()).ToList() ?? new List<ReservationDto>()
        );


        public static ReservationDto AsDto(this ReservationReadModel model)
        => new(
            model.Id,
            model.UserId,
            model.VehicleId,
            model.PickupPointId,
            model.ReturnPointId,
            model.StartDate,
            model.EndDate,
            model.ReturnedDate,
            model.Status
        );
    }
}
