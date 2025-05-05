using BBIS.Application.Contracts;
using BBIS.Common.Exceptions;
using BBIS.Common;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BBIS.Application.DTOs.Signatory;
using BBIS.Application.DTOs.Common;

namespace BBIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatoryController : ApiControllerBase
    {
        private readonly ISignatoryService signatoryService;

        public SignatoryController(ISignatoryService signatoryService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.signatoryService = signatoryService;
        }

        [HttpPost]
        public async Task<ActionResult<RequestResult<Guid>>> UpsertSignatory([FromBody] SignatoryDto dto)
        {
            try
            {
                var result = await this.signatoryService.UpsertSignatory(dto);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating signatory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var action = dto.SignatoryId.HasValue ? "updating" : "creating";
                logger.LogError($"Something went wrong {action} signatory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestResult>> DeleteSignatory(Guid id)
        {
            try
            {
                await this.signatoryService.DeleteSignatory(id);
                return this.Json();
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong deleting signatory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong deleting signatory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestResult<SignatoryViewDto>>> GetSignatory(Guid id)
        {
            try
            {
                var result = await this.signatoryService.GetSignatory(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went retrieving signatory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving signatory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("list")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<SignatoryViewDto>>>> GetSignatoryList([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.signatoryService.GetPagedListAsync(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving signatory list: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("options")]
        public async Task<ActionResult<RequestResult<List<SignatoryOptionDto>>>> GetSignatoryOptions()
        {
            try
            {
                var result = await this.signatoryService.GetSignatoryOptions();
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving signatories: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
