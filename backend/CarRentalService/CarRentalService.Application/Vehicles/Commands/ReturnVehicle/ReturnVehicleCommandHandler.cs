using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using CarRentalService.Domain.Services.Interfaces;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Vehicles.Commands.ReturnVehicle
{
    public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand, Result>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleDomainService _vehicleDomainService;

        public ReturnVehicleCommandHandler(IVehicleRepository vehicleRepository, IVehicleDomainService vehicleDomainService)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleDomainService = vehicleDomainService;
        }

        public async Task<Result> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByReservationIdAsync(request.ReservationId, cancellationToken);
            if (vehicle is null)
                return Result.Fail(ApplicationErrors.Reservation.NotFound);

            var result = await _vehicleDomainService.ReturnVehicleAsync(vehicle, request.ReservationId, request.UserId, cancellationToken);

            return result;
        }
    }
}
