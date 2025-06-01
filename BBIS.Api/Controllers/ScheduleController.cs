using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Application.DTOs.Schedule;
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
    [Authorize]
    public class ScheduleController : ApiControllerBase
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.scheduleService = scheduleService;
        }

        [HttpPost("schedule")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<DonorListDto>>>> GetSchedules([FromBody] ScheduleDto dto)
        {
            try
            {
                var results = await this.scheduleService.GetSchedules(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving registered schedules: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("schedule/{id}")]
        public async Task<ActionResult<RequestResult<ScheduleDto>>> GetScheduleById(Guid id)
        {
            try
            {
                var result = await this.scheduleService.GetScheduleById(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving Initial Screening info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving Initial Screening info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("upsert-schedule")]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateSchedule([FromBody] ScheduleDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.scheduleService.CreateUpdateSchedule(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during schedule creation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during schedule creation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }


}
