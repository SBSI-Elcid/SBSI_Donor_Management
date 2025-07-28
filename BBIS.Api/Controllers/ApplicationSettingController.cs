using BBIS.Application.Contracts;
using BBIS.Application.DTOs.ApplicationSetting;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
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
    public class ApplicationSettingController : ApiControllerBase
    {
        private readonly IApplicationSettingService applicationSettingService;

        public ApplicationSettingController(IApplicationSettingService applicationSettingService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.applicationSettingService = applicationSettingService;
        }

        /// <summary>
        /// This retrieves the list of application settings.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// 
        [HttpPost("roles")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<RoleDto>>>> GetRoleSettings([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.applicationSettingService.GetRoleSettings(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving the list of application settings: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("all-roles")]
        public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
        {
            try
            {
                var results = await this.applicationSettingService.GetAllRoles();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error retrieving roles: {ex}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("questionnare")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<MedicalQuestionnaireDto>>>> GetQuestionnareSettings([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.applicationSettingService.GetQuestionnaireSettings(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving the list of application settings: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("settings")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<ApplicationSettingDto>>>> GetApplicationSettings([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.applicationSettingService.GetApplicationSettings(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving the list of application settings: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the list of blood component settings.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("settings/bloodComponents")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<BloodComponentSettingDto>>>> GetBloodComponentSettings([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.applicationSettingService.GetBloodComponentSettings(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving the list of blood component settings: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the list of institutions.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("settings/institutions")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<InstitutionDto>>>> GetInstitutions([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.applicationSettingService.GetInstitutions(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving the list of institutions: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the list of blood component checklists.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("settings/checklists")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<BloodComponentChecklistListDto>>>> GetBloodComponentChecklists([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.applicationSettingService.GetBloodComponentChecklists(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving the list of blood component checklists: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the list of test order types
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("settings/testOrderTypes")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<TestOrderTypeSettingDto>>>> GetTestOrderTypes([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.applicationSettingService.GetTestOrderTypes(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving the list of test order types: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the application settings by their setting keys.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("bysettingkeys")]
        public async Task<ActionResult<RequestResult<List<ApplicationSettingKeyValuePair>>>> GetApplicationSettingsByKey(ApplicationSettingRequestDto dto)
        {
            try
            {
                var result = await this.applicationSettingService.GetApplicationSettingsByKey(dto.SettingKeys);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving application setting: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the institution details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("institution/{id}")]
        public async Task<ActionResult<RequestResult<InstitutionDto>>> GetInstitution(Guid id)
        {
            try
            {
                var result = await this.applicationSettingService.GetInstitutionById(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went retrieving institution: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving institution: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the blood component name and its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("bloodComponents")]
        public async Task<ActionResult<RequestResult<List<BloodComponentDto>>>> GetBloodComponents()
        {
            try
            {
                var result = await this.applicationSettingService.GetBloodComponents();
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving blood components: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the blood component checklist by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("checklist/{id}")]
        public async Task<ActionResult<RequestResult<BloodComponentChecklistDto>>> GetBloodComponentChecklist(Guid id)
        {
            try
            {
                var result = await this.applicationSettingService.GetBloodComponentChecklistById(id);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving blood components: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the test order type by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("testOrderType/{id}")]
        public async Task<ActionResult<RequestResult<BloodComponentChecklistDto>>> GetTestOrderType(Guid id)
        {
            try
            {
                var result = await this.applicationSettingService.GetTestOrderTypeById(id);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went retrieving test order type: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This updates the application setting
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<RequestResult<Guid>>> UpdateApplicationSetting(ApplicationSettingDto dto)
        {
            try
            {
                var result = await this.applicationSettingService.UpdateApplicationSetting(dto);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong updating application setting: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This adds a new or updates an existing blood component setting.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("upsert-bloodcomponent")]
        public async Task<ActionResult<RequestResult<Guid>>> UpsertBloodComponentSetting([FromBody] BloodComponentSettingDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.applicationSettingService.UpsertBloodComponentSetting(dto);
                return this.Json(result);
            }
            catch (RecordExistException ex)
            {
                logger.LogError($"Something went wrong creating blood component: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating blood component: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var action = dto.BloodComponentId.HasValue ? "updating" : "creating";
                logger.LogError($"Something went wrong {action} blood component: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("upsert-librariesrole")]
        public async Task<ActionResult<RequestResult<Guid>>> UpsertLibrariesRole([FromBody] RoleDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.applicationSettingService.UpsertLibrariesRole(dto);
                return this.Json(result);
            }
 
            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("upsert-librariesquestionnare")]
        public async Task<ActionResult<RequestResult<int>>> UpsertLibrariesQuestionnare([FromBody] MedicalQuestionnaireDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.applicationSettingService.UpsertLibrariesQuestionnare(dto);
                return this.Json(result);
            }

            catch (Exception ex)
            {
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// This adds a new or updates an existing institution.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("upsert-institution")]
        public async Task<ActionResult<RequestResult<Guid>>> UpsertInstitutions([FromBody] InstitutionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.applicationSettingService.UpsertInstitution(dto);
                return this.Json(result);
            }
            catch (RecordExistException ex)
            {
                logger.LogError($"Something went wrong creating institution: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating institution: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var action = dto.InstitutionId.HasValue ? "updating" : "creating";
                logger.LogError($"Something went wrong {action} institution: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This adds a new or updates an existing blood component checklist.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("upsert-checklist")]
        public async Task<ActionResult<RequestResult<Guid>>> UpsertBloodComponentChecklist([FromBody] BloodComponentChecklistDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.applicationSettingService.UpsertBloodComponentChecklist(dto);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating checklist: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var action = dto.BloodComponentChecklistId.HasValue ? "updating" : "creating";
                logger.LogError($"Something went wrong {action} institution: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This adds a new or updates an existing test order type
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("upsert-testOrderType")]
        public async Task<ActionResult<RequestResult<Guid>>> UpsertTestOrderType([FromBody] TestOrderTypeSettingDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.applicationSettingService.UpsertTestOrderType(dto);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating test order type: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var action = dto.TestOrderTypeId.HasValue ? "updating" : "creating";
                logger.LogError($"Something went wrong {action} test order type: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// Check if the app is Online or Offline by returning True if Offline else False
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-app-status")]
        public async Task<ActionResult<RequestResult<bool>>> GetAppStatus()
        {
            try
            {
                return this.Json(this.IsOffline);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went getting app status: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
