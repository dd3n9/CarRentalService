using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.Reservations.Commands.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRentalPointRepository _rentalPointRepository;

        public CreateReservationCommandHandler(IVehicleRepository vehicleRepository,
            IUserRepository userRepository,
            IRentalPointRepository rentalPointRepository)
        {
            _vehicleRepository = vehicleRepository;
            _userRepository = userRepository;
            _rentalPointRepository = rentalPointRepository;
        }

        public async Task<Result> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);
            if (vehicle is null)
                return Result.Fail(ApplicationErrors.Vehicle.NotFound);

            //var pickupPoint = await _rentalPointRepository.GetByIdAsync(request.PickupPointId, cancellationToken);
            //if (pickupPoint is null)
            //    return Result.Fail(ApplicationErrors.RentalPoint.NotFound);

            var returnPoint = await _rentalPointRepository.GetByIdAsync(request.ReturnPointId, cancellationToken);
            if (returnPoint is null)
                return Result.Fail(ApplicationErrors.RentalPoint.NotFound);

            var reservationResult = vehicle.Reserve(request.UserId, vehicle.RentalPointId, returnPoint.Id, request.StartDate, request.EndDate);

            await _vehicleRepository.SaveChangesAsync(cancellationToken);

            return reservationResult.ToResult();
        }
    }
}
