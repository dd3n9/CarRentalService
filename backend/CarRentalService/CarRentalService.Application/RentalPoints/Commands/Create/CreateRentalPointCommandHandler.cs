using CarRentalService.Domain.Common.Errors;
using CarRentalService.Domain.RentalPointAggregate;
using CarRentalService.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.RentalPoints.Commands.Create
{
    public class CreateRentalPointCommandHandler : IRequestHandler<CreateRentalPointCommand, Result>
    {
        private readonly IRentalPointRepository _rentalPointRepository;

        public CreateRentalPointCommandHandler(IRentalPointRepository rentalPointRepository)
        {
            _rentalPointRepository = rentalPointRepository;
        }

        public async Task<Result> Handle(CreateRentalPointCommand request, CancellationToken cancellationToken)
        {
            var isRentalPointExists = await _rentalPointRepository.GetByNameAsync(request.Name, cancellationToken);
            if (isRentalPointExists is not null)
                return Result.Fail(ApplicationErrors.RentalPoint.AlreadyExists);

            var rentalPoint = RentalPoint.Create(
                    request.Name,
                    request.City,
                    request.Street
                );

            await _rentalPointRepository.AddAsync(rentalPoint, cancellationToken);
            await _rentalPointRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
