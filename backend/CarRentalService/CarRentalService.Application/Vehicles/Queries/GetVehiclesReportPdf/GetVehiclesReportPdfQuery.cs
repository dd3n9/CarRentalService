using MediatR;

namespace CarRentalService.Application.Vehicles.Queries.GetVehiclesReportPdf
{
    public record GetVehiclesReportPdfQuery(string? City,
        int? YearFrom,
        int? YearTo,
        int? Seats,
        string VehicleType
        ) : IRequest<byte[]>;
}
