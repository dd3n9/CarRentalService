using CarRentalService.Application.Vehicles.Queries.GetVehiclesReportPdf;
using CarRentalService.Infrastructure.EF.Context;
using CarRentalService.Infrastructure.EF.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Queries.Vehicles
{
    internal class GetVehiclesReportPdfQueryHandler : IRequestHandler<GetVehiclesReportPdfQuery, byte[]>
    {
        private readonly DbSet<VehicleReadModel> _vehicles;

        public GetVehiclesReportPdfQueryHandler(ReadDbContext readDbContext)
        {
            _vehicles = readDbContext.Vehicles;
        }

        public async Task<byte[]> Handle(GetVehiclesReportPdfQuery request, CancellationToken cancellationToken)
        {
            IQueryable<VehicleReadModel> query = _vehicles
               .Include(v => v.Reservations)
               .Include(v => v.RentalPoint);

            if(!string.IsNullOrWhiteSpace(request.City))
            {
                query = query.Where(v => v.RentalPoint.Address.Contains(request.City));
            }

            if(!string.IsNullOrWhiteSpace(request.VehicleType))
            {
                query = query.Where(v => v.Type == request.VehicleType);
            }

            if(request.Seats.HasValue)
            {
                query = query.Where(v => v.Seats == request.Seats);
            }

            if(request.YearFrom.HasValue)
            {
                query = query.Where(v => v.Year >= request.YearFrom);
            }

            if(request.YearTo.HasValue)
            {
                query = query.Where(v => v.Year <= request.YearTo);
            }

            var vehicles = await query.ToListAsync();

            if(vehicles == null || vehicles.Count == 0)
            {
                return null;
            }

            return GeneratePdf(vehicles);
        }
        private byte[] GeneratePdf(List<VehicleReadModel> vehicles)
        {
            using var stream = new MemoryStream();
            var document = new Document(PageSize.A4.Rotate());
            PdfWriter.GetInstance(document, stream);
            document.Open();

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var tableFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            document.Add(new Paragraph("Available Vehicles", titleFont));
            document.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(8) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 2, 2, 2, 3, 2, 2, 2, 3 });

            table.AddCell(CreateCell("Brand", tableFont, true));
            table.AddCell(CreateCell("Model", tableFont, true));
            table.AddCell(CreateCell("Price/Day", tableFont, true));
            table.AddCell(CreateCell("Type", tableFont, true));
            table.AddCell(CreateCell("License Plate", tableFont, true));
            table.AddCell(CreateCell("Year", tableFont, true));
            table.AddCell(CreateCell("Seats", tableFont, true));
            table.AddCell(CreateCell("Address", tableFont, true));

            foreach (var vehicle in vehicles)
            {
                table.AddCell(CreateCell(vehicle.Brand, tableFont));
                table.AddCell(CreateCell(vehicle.Model, tableFont));
                table.AddCell(CreateCell($"{vehicle.PricePerDay} USD", tableFont));
                table.AddCell(CreateCell(vehicle.Type, tableFont));
                table.AddCell(CreateCell(vehicle.LicensePlate, tableFont));
                table.AddCell(CreateCell(vehicle.Year.ToString(), tableFont));
                table.AddCell(CreateCell(vehicle.Seats.ToString(), tableFont));
                table.AddCell(CreateCell(vehicle.RentalPoint.Address, tableFont)); 
            }

            document.Add(table);
            document.Close();

            return stream.ToArray();
        }

        private PdfPCell CreateCell(string text, Font font, bool isHeader = false)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5,
                BackgroundColor = isHeader ? BaseColor.LIGHT_GRAY : BaseColor.WHITE
            };
            return cell;
        }
    }
}
