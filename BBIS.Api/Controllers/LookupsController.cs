using BBIS.Application.Contracts;
using BBIS.Common.Exceptions;
using BBIS.Common;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BBIS.Application.DTOs.Lookup;

namespace BBIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LookupsController : ApiControllerBase
    {
        private readonly ILookupService lookupService;

        public LookupsController(ILookupService lookupService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.lookupService = lookupService;
        }

        [HttpPost("bylookupkeys")]
        [AllowAnonymous]
        public async Task<ActionResult<RequestResult<List<LookupDto>>>> GetLookups(LookupRequesDto dto)
        {
            try
            {
                var result = await this.lookupService.GetLookups(dto.LookupKeys);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving lookups: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
