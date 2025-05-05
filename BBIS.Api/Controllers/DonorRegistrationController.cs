using BBIS.Application.Contracts;
using BBIS.Common.Exceptions;
using BBIS.Common;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;

namespace BBIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DonorRegistrationController : ApiControllerBase
    {
        private readonly IDonorRegistrationService donorRegistrationService;

        public DonorRegistrationController(IDonorRegistrationService donorRegistrationService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.donorRegistrationService = donorRegistrationService;
        }

        /// <summary>
        /// This retrieves all the medical history questionnaires.
        /// </summary>
        /// <returns>Returns list of medical history questionnaires</returns>
        [AllowAnonymous]
        [HttpGet("questionnaires")]
        public async Task<ActionResult<RequestResult<List<MedicalQuestionnaireDto>>>> GetQuestionnaires()
        {
            try
            {
                var result = await this.donorRegistrationService.GetMedicalQuestionnaires();
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving questionnaires: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the list of registered donors.
        /// </summary>
        /// <param name="dto">This contains the searched text, page number, page size, sort by and sort direction.</param>
        /// <returns>Returns the paged result of the registered donors</returns>
        [HttpPost("registereddonors")]
        [Authorize(Policy = ApplicationRoles.DonorScreeningPolicy)]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<RegisteredDonorDto>>>> GetRegisteredDonors([FromBody] PagedSearchDto dto)
        {
            try
            {
                var results = await this.donorRegistrationService.GetRegisteredDonors(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving registered donors: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the registered donor information.
        /// </summary>
        /// <param name="id">This contains the donor registration id.</param>
        /// <returns>Returns the registered donor info</returns>
        [HttpGet("info/{id}")]
        [Authorize(Policy = ApplicationRoles.DonorScreeningPolicy)]
        public async Task<ActionResult<RequestResult<RegisteredDonorInfoDto>>> GetRegisteredDonorInfo(Guid id)
        {
            try
            {
                var result = await this.donorRegistrationService.GetRegisteredDonorInfo(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving Donor Registration Information: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving Donor Registration Information: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This endpoint is used to save the info of the donor that just registered.
        /// </summary>
        /// <param name="dto">This contains the donor info including the medical history answers.</param>
        /// <returns>Returns the registration number</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RequestResult<string>>> RegisterDonor([FromBody] RegisterDonorDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorRegistrationService.RegisterDonor(dto);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong with Donor registration: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong with Donor registration: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This verifies if a person can register as a donor or not.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Returns donor deferral status if there's any and remarks</returns>
        [HttpPost("verify")]
        //[Authorize(Policy = ApplicationRoles.CreateDonorPolicy)]
        [AllowAnonymous]
        public async Task<ActionResult<RequestResult<VerifyDonorResultDto>>> VerifyDonor([FromBody] VerifyDonorDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorRegistrationService.VerifyDonor(dto);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong verifying donor: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This updates and create the donor and its donor transaction. 
        /// This is called after successful assessment of the registered donor and is ready to be screened.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Returns the transaction id</returns>
        [HttpPost("updatecreate-transaction")]
        [Authorize(Policy = ApplicationRoles.DonorScreeningPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> UpdateDonorAndCreateTransaction([FromBody] UpdateDonorInfoDto dto)
        {
            try
            {
                var result = await this.donorRegistrationService.UpdateDonorAndCreateTransaction(dto, this.UserId, this.IsOffline);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during donor transaction creation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during donor transaction creation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
