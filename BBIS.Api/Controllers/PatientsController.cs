using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Patient;
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
    public class PatientsController : ApiControllerBase
    {
        private readonly IPatientService patientService;

        public PatientsController(IPatientService patientService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.patientService = patientService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<RequestResult<PatientResult>>> Create([FromBody] PatientDto dto)
        {
            try
            {
                var result = await this.patientService.CreatePatient(dto, this.UserId);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong creating donor test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult<RequestResult<PatientResult>>> Update([FromBody] PatientDto dto)
        {
            try
            {
                var result = await this.patientService.UpdatePatient(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating patient: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong updating patient: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("list")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<PatientViewDto>>>> GetList([FromBody] PatientPagedSearchDto dto)
        {
            try
            {
                var results = await this.patientService.GetPagedListAsync(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving patient list: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestResult<PatientDto>>> GetPatientById(Guid id)
        {
            try
            {
                var result = await this.patientService.GetPatientById(id);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving patient: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
