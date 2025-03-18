using Asp.Versioning;
using CarRentalService.Application.Authentication.Register;
using CarRentalService.Contracts.Authentication;
using CarRentalService.Contracts.Configurations;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CarRentalService.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : BaseController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly CookiesConfig _cookiesConfig;
        private readonly JwtConfig _jwtConfig;


        public AuthenticationController(ISender mediator,
            IMapper mapper,
            IOptions<CookiesConfig> cookiesConfig,
            IOptions<JwtConfig> jwtConfig)
        {
            _mediator = mediator;
            _mapper = mapper;
            _cookiesConfig = cookiesConfig.Value;
            _jwtConfig = jwtConfig.Value;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var registeResult = await _mediator.Send(command);

            return OkOrNotFound(registeResult);
        }
    }
}
