using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Application.DTOs.DonorScreening;
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
    public class DonorScreeningController : ApiControllerBase
    {
        private readonly IDonorScreeningService donorScreeningService;

        public DonorScreeningController(IDonorScreeningService donorScreeningService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.donorScreeningService = donorScreeningService;
        }

        [HttpPost("donors")]
        [Authorize(Policy = ApplicationRoles.DonorScreeningPolicy)]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<DonorListDto>>>> GetDonors([FromBody] DonorPagedSearchDto dto)
        {
            try
            {
                var results = await this.donorScreeningService.GetDonors(dto, this.UserRoles);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving registered donors: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("initialscreening/{id}")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<DonorInitialScreeningDto>>> GetInitialScreeningInfo(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetInitialScreeningInfo(id);
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

        [HttpGet("scheduleactivityid/{id}")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<Guid?>>> GetScheduleIdFromTransaction(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetScheduleIdFromTransaction(id);
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

        [HttpGet("vitalsigns/{id}")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<DonorVitalSignsDto>>> GetDonorVitalSignsInfo(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetDonorVitalSignsInfo(id);
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

        [HttpGet("issuanceofbloodbag/{id}")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<DonorBloodBagIssuanceDto>>> getDonorBloodBagIssuance(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetDonorBloodBagIssuance(id);
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

        [HttpGet("counseling/{id}")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<DonorCounselingDto>>> GetDonorCounselingInfo(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetDonorCounselingInfo(id);
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

        [HttpGet("initialscreening/recentdonation/{id}")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<List<DonorRecentDonationDto>>>> GetDonorRecentDonations(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetRecentDonations(id);
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

        [HttpGet("physicalexam/{id}")]
        [Authorize(Policy = ApplicationRoles.PhysicalExaminationPolicy)]
        public async Task<ActionResult<RequestResult<DonorPhysicalExaminationDto>>> GetPhysicalExamInfo(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetPhysicalExamInfo(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving Physical Exam info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving Physical Exam info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("bloodcollection/{id}")]
        [Authorize(Policy = ApplicationRoles.BloodCollectionPolicy)]
        public async Task<ActionResult<RequestResult<DonorBloodCollectionDto>>> GetBloodCollectionInfo(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetBloodCollectionInfo(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving Blood Collection info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving Blood Collection info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("postdonationcare/{id}")]
        [Authorize(Policy = ApplicationRoles.BloodCollectionPolicy)]
        public async Task<ActionResult<RequestResult<DonorBloodCollectionDto>>> GetPostDonationCare(Guid id)
        {
            try
            {
                var result = await this.donorScreeningService.GetPostDonationCare(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving Blood Collection info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving Blood Collection info: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("upsert-initialscreening")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateInitialScreening([FromBody] DonorInitialScreeningDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
              
                var result = await this.donorScreeningService.CreateUpdateDonorInitialScreening(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during initial screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during initial screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        //[HttpPost("upsert-medical")]
        //[Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        //public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateDonorVitalSigns([FromBody] DonorVitalSignsDto dto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var result = await this.donorScreeningService.CreateUpdateDonorVitalSigns(dto, this.UserId);
        //        return this.Json(result);
        //    }
        //    catch (RecordNotFoundException ex)
        //    {
        //        logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
        //        return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
        //        return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        [HttpPost("upsert-counseling/")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateDonorCounseling([FromBody] DonorCounselingDto dto, Guid id)
        {
            Console.WriteLine("Hit endpoint");
            Console.WriteLine($"Hit endpoint2 {dto}");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorScreeningService.CreateUpdateDonorCounseling(dto, id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("upsert-vitalsigns")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateDonorVitalSigns([FromBody] DonorVitalSignsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorScreeningService.CreateUpdateDonorVitalSigns(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("upsert-bloodbagissuance")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateDonorBloodBagIssuance([FromBody] DonorBloodBagIssuanceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorScreeningService.CreateUpdateDonorBloodBagIssuance(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during bloodIssuance signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("u-methodbloodcollection")]
        [Authorize(Policy = ApplicationRoles.InitialScreeningPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> UpdateDonorTransactionForBloodCollection([FromBody] RegisteredDonorDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorScreeningService.UpdateDonorTransactionForBloodCollection(dto);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during vital signs screening: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("upsert-physicalexamination")]
        [Authorize(Policy = ApplicationRoles.PhysicalExaminationPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdatePhysicalExamination([FromBody] DonorPhysicalExaminationDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorScreeningService.CreateUpdateDonorPhysicalExamination(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during physical examination: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during physical examination: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("upsert-bloodcollection")]
        [Authorize(Policy = ApplicationRoles.BloodCollectionPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateBloodCollection([FromBody] DonorBloodCollectionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorScreeningService.CreateUpdateDonorBloodCollection(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during blood collection: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during blood collection: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("upsert-donorpostdonationcare")]
        [Authorize(Policy = ApplicationRoles.BloodCollectionPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateUpdateDonorPostDonationCare([FromBody] DonorPostDonationCareDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await this.donorScreeningService.CreateUpdateDonorPostDonationCare(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong during blood collection: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during blood collection: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
