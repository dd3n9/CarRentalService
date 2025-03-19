using CarRentalService.Application.Authentication.Commands.RefreshToken;
using CarRentalService.Contracts.Configurations;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Controllers.V1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        public async Task<IActionResult> Test(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
