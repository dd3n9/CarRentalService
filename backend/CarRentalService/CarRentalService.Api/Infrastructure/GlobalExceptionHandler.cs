using CarRentalService.Contracts.Common.Validation;
using CarRentalService.Domain.Common.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var problemDetails = CreateProblemDetails(exception);

            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private static ProblemDetails CreateProblemDetails(Exception exception)
        {
            ProblemDetails problemDetails = exception switch
            {
                CustomException ddFilmEx => CreateProblemDetails(StatusCodes.Status404NotFound,
                    "Server Error", exception.Message),
                CustomValidationException => CreateProblemDetails(StatusCodes.Status400BadRequest,
                    "Validation error", "One or more validation errors occurred"),
                OperationCanceledException => CreateProblemDetails(StatusCodes.Status499ClientClosedRequest,
                    "Closed Request", "The request was cancelled"),
                _ => CreateProblemDetails(StatusCodes.Status500InternalServerError,
                    "Internal Server Error", "An unexpected error occurred")
            };

            if (exception is CustomValidationException customValidationException)
            {
                problemDetails.Extensions["errors"] = customValidationException.ValidationErrors;
            }

            return problemDetails;
        }

        private static ProblemDetails CreateProblemDetails(int status, string title, string detail)
        {
            return new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail
            };
        }
    }
}
