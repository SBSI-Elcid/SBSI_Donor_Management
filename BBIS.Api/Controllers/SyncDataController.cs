using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Reports;
using BBIS.Common;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BBIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SyncDataController : ApiControllerBase
    {
        private readonly ISyncDataService syncDataService;
        
        public SyncDataController(ISyncDataService syncDataService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.syncDataService = syncDataService;
        }

        [HttpPost("sync-donors")]
        public async Task<ActionResult<RequestResult<string>>> SyncDonors()
        {
            try
            {
                if(!this.IsOffline)
                {
                    throw new Exception("Cannot sync donor when the App is online");
                }

                var result = await this.syncDataService.SyncDonors();
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong synching donors: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

       
    }
}
