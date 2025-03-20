using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.RentalPoints.Commands.Delete
{
    public class DeleteRentalPointCommandHandler : IRequestHandler<DeleteRentalPointCommand, Result>
    {
        private readonly IRentalPointRepository _rentalPointRepository;

        public DeleteRentalPointCommandHandler(IRentalPointRepository rentalPointRepository)
        {
            _rentalPointRepository = rentalPointRepository;
        }

        public async Task<Result> Handle(DeleteRentalPointCommand request, CancellationToken cancellationToken)
        {
            var rentalPoint = await _rentalPointRepository.GetByIdAsync(request.rentalPointId, cancellationToken);
            if (rentalPoint is null)
                return Result.Fail(ApplicationErrors.RentalPoint.NotFound);

            _rentalPointRepository.Delete(rentalPoint, cancellationToken);
            await _rentalPointRepository.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
