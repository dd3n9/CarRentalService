using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Reservations.Commands.Delete
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Result>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DeleteReservationCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;   
        }

        public async Task<Result> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByReservationIdAsync(request.ReservationId, cancellationToken);
            if(vehicle is null) 
            {
                return Result.Fail(ApplicationErrors.Reservation.NotFound);
            }

            var result = vehicle.RemoveReservation(request.ReservationId);
            if (result.IsSuccess)
            {
                _vehicleRepository.Update(vehicle);
                await _vehicleRepository.SaveChangesAsync(cancellationToken);
            }

            return result;
        }
    }
}
