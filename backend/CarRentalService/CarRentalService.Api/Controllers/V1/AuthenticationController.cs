using Asp.Versioning;
using CarRentalService.Application.Authentication.Commands.Register;
using CarRentalService.Application.Authentication.Queries.Login;
using CarRentalService.Contracts.Authentication;
using CarRentalService.Contracts.Configurations;
using FluentResults;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(command);

            var test = Request.Cookies[_cookiesConfig.CookiesKey];

            if (authResult.IsSuccess)
            {
                HttpContext.Response.Cookies.Append(_cookiesConfig.CookiesKey, authResult.Value.RefreshToken,
                    new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMonths(_jwtConfig.RefreshTokenExpiryMonths)
                    });


                var response = new
                {
                    authResult.Value.AuthenticationDto,
                    authResult.Value.Token
                };

                return OkOrNotFound(Result.Ok(response));
            }

            return OkOrNotFound(authResult);
        }
    }
}
