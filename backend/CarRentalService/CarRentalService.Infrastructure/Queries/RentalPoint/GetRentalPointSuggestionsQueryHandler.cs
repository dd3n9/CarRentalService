using CarRentalService.Application.RentalPoints.Queries;
using CarRentalService.Contracts.RentalPoints.Responses;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Models;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Queries.RentalPoint
{
    internal class GetRentalPointSuggestionsQueryHandler : IRequestHandler<GetRentalPointSuggestionsQuery, Result<List<RentalPointResponse>>>
    {
        private readonly DbSet<RentalPointReadModel> _rentalPoints;

        public GetRentalPointSuggestionsQueryHandler(ReadDbContext readDbContext)
        {
            _rentalPoints = readDbContext.RentalPoints;
        }

        public async Task<Result<List<RentalPointResponse>>> Handle(GetRentalPointSuggestionsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Prefix))
            {
                return Result.Ok(new List<RentalPointResponse>()); 
            }

            var suggestions = await _rentalPoints
                .Where(rp => Microsoft.EntityFrameworkCore.EF.Functions.Like(rp.Name, $"{request.Prefix}%"))
                .OrderBy(rp => rp.Address)
                .Take(request.MaxResults)
                .Select(rp => new RentalPointResponse(
                    rp.Id,
                    rp.Name,
                    rp.Address 
                ))
                .ToListAsync(cancellationToken);

            return Result.Ok(suggestions);
        }
    }
}
