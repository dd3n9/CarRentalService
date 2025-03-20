using CarRentalService.Contracts.RentalPoints.Responses;
using FluentResults;
using MediatR;

namespace CarRentalService.Application.RentalPoints.Queries
{
    public record GetRentalPointSuggestionsQuery(
            string Prefix, 
            int MaxResults = 10
        ) : IRequest<Result<List<RentalPointResponse>>>;
}
