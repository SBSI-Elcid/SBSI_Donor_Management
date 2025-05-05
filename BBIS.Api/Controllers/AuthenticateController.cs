using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Authentication;
using BBIS.Common;
using BBIS.Common.Exceptions;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BBIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ApiControllerBase
    {
        private readonly IAuthenticationService authenticationService;
     
        public AuthenticateController(IAuthenticationService authenticationService, ILoggerManager logger, IConfiguration configuration): base(logger, configuration)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<RequestResult<AuthenticationResultDto>>> Authenticate([FromBody] LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await authenticationService.AuthenticateUser(dto);

                return this.Json(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong creating user account: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("renew")]
        [AllowAnonymous]
        public async Task<ActionResult<RequestResult<RefreshTokenResult>>> Renew([FromBody] RefreshTokenRequestDto dto)
        {
            try
            {
                var result = await authenticationService.RefreshToken(dto);

                return this.Json(result, HttpStatusCode.OK);
            }
            catch (RefreshTokenException ex)
            {
                logger.LogError($"Something went wrong renewing token: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong renewing token: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
