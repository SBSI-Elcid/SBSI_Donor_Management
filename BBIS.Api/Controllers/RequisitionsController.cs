using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Application.DTOs.Requisition;
using BBIS.Application.DTOs.TestOrder;
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
    public class RequisitionsController : ApiControllerBase
    {
        private readonly IRequisitionService requisitionService;

        public RequisitionsController(IRequisitionService requistionService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.requisitionService = requistionService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<RequestResult<Guid>>> CreateReservantion([FromBody] ReservationDto dto)
        {
            try
            {
                var result = await this.requisitionService.CreateReservation(dto, this.UserId);
                return this.Json(result);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError($"Something went wrong creating reservation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong creating reservation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong creating reservation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult<RequestResult<Guid>>> UpdateReservantion([FromBody] UpdateReservationDto dto)
        {
            try
            {
                var result = await this.requisitionService.UpdateReservation(dto, this.UserId);
                return this.Json(result);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError($"Something went wrong updating reservation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating reservation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong updating reservation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("check-list")]
        public async Task<ActionResult<RequestResult<List<CheckListOptionDto>>>> GetReservationChecklist()
        {
            try
            {
                var results = await this.requisitionService.GetReservationChecklist();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving reservation checklist: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the list of reservations.
        /// </summary>
        /// <param name="dto">This contains the searched text, page number, page size, sort by and sort direction.</param>
        /// <returns>Returns the paged result of the registered donors</returns>
        [HttpPost("reservations")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<ReservationListingDto>>>> GetReservations([FromBody] RequisitionPagedSearchDto dto)
        {
            try
            {
                var results = await this.requisitionService.GetReservations(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving reservations: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the transfusion details of a reservation
        /// </summary>
        /// <param name="id">This contains the reservation id.</param>
        /// <returns>Returns the transfusion details</returns>
        [HttpGet("transfusion/{id}")]
        public async Task<ActionResult<RequestResult<TransfusionViewDetailsDto>>> GetTransfusionDetailsById(Guid id)
        {
            try
            {
                var results = await this.requisitionService.GetTransfusionDetails(id);
                return this.Json(results);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving transfusion details: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving transfusion details: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This creates/updates the transfusion details.
        /// </summary>
        /// <param name="dto">This contains the list of transfusion details per component</param>
        /// <returns></returns>
        [HttpPost("transfusion/upsert")]
        public async Task<ActionResult<RequestResult<Guid>>> UpsertTransfusionDetails([FromBody] List<TransfusionDto> dto)
        {
            try
            {
                var results = await this.requisitionService.UpsertTransfusionDetails(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong during transfusion details creation: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// This retrieves the reservation matching donors
        /// </summary>
        /// <param name="id">This contains the reservation id.</param>
        /// <returns>Returns the transfusion details</returns>
        [HttpGet("reservation-donors/{id}")]
        public async Task<ActionResult<RequestResult<List<CreateCrossMatchOrderDto>>>> GetReservationDonors(Guid id)
        {
            try
            {
                var results = await this.requisitionService.GetReservationMatchingDonors(id);
                return this.Json(results);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving reservation donors: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving reservation donors: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        //[HttpGet("status")]
        //public async Task<ActionResult<RequestResult<List<InventoryItemDto>>>> GetInvetoryStatus()
        //{
        //    try
        //    {
        //        var results = await this.requisitionService.GetInventoryStatus();
        //        return this.Json(results);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Something went wrong retrieving inventory: {ex.Message}");
        //        return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

        //[HttpPost("get-items")]
        //public async Task<ActionResult<RequestResult<List<InventoryListDto>>>> GetInventoryItems([FromBody] InventoryFilterDto dto)
        //{
        //    try
        //    {
        //        var results = await this.requisitionService.GetInventoryItems(dto);
        //        return this.Json(results);
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        logger.LogError($"Something went wrong retrieving inventory items: {ex.Message}");
        //        return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Something went wrong retrieving inventory items: {ex.Message}");
        //        return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}

    }
}
