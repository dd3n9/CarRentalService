using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using CarRentalService.Api.Extensions;
using CarRentalService.Contracts.Common;

namespace CarRentalService.Api.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult OkOrNotFound<TResult>(Result<TResult> result)
        {
            if (result.IsFailed)
            {
                bool isAuthenticationError = result.Errors.Any(e =>
                    e.Metadata.TryGetValue("ErrorCode", out var errorCodeObj) &&
                    errorCodeObj is string errorCode &&
                    errorCode.StartsWith("Authentication.")
                );

                if (isAuthenticationError)
                {
                    return Unauthorized(new
                    {
                        Errors = result.Errors.Select(e => e.Message)
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Errors = result.Errors.Select(e => e.Message)
                    });
                }
            }
            var valueType = result.Value?.GetType();
            if (valueType != null && valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(PaginatedList<>))
            {
                dynamic paginatedResult = result.Value;

                return new ResultsExtensions.PaginationResult(
                    paginatedResult.PageSize,
                    paginatedResult.CurrentPage,
                    paginatedResult.TotalCount,
                    paginatedResult.TotalPages,
                    paginatedResult.Items
                );
            }

            return Ok(result.Value);
        }

        protected ActionResult OkOrNotFound(Result result)
        {
            if (result.IsFailed)
            {
                bool isAuthenticationError = result.Errors.Any(e =>
                    e.Metadata.TryGetValue("ErrorCode", out var errorCodeObj) &&
                    errorCodeObj is string errorCode &&
                    errorCode.StartsWith("Authentication.")
                );

                if (isAuthenticationError)
                {
                    return Unauthorized(new
                    {
                        Errors = result.Errors.Select(e => e.Message)
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Errors = result.Errors.Select(e => e.Message)
                    });
                }
            }

            return Ok();
        }

        private bool IsPaginatedList(object value, out object paginatedResult)
        {
            paginatedResult = null;

            if (value == null)
                return false;

            var type = value.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(PaginatedList<>))
            {
                paginatedResult = value;
                return true;
            }

            return false;
        }
    }
}
