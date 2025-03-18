using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarRentalService.Api.Extensions
{
    public static class ResultsExtensions
    {
        public static ActionResult OkPaginationResult(this IResultExtensions resultExtensions,
            int pageSize, int pageNumber, int totalItems, int totalPages, IEnumerable<object> items)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions);
            return new PaginationResult(pageSize, pageNumber, totalItems, totalPages, items);
        }

        public class PaginationResult(
            int pageSize,
            int pageNumber,
            int totalItems,
            int totalPages,
            IEnumerable<object> items) : ActionResult
        {
            public override async Task ExecuteResultAsync(ActionContext context)
            {
                var header = new
                {
                    pageSize,
                    pageNumber,
                    totalItems,
                    totalPages
                };

                context.HttpContext.Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(header));
                context.HttpContext.Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;

                await context.HttpContext.Response.WriteAsJsonAsync(items);
            }
        }
    }
}
