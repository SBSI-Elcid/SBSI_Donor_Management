using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorTestOrder;
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
    public class TestOrdersController : ApiControllerBase
    {
        private readonly IDonorTestOrderService donorTestOrderService;
        private readonly ITestOrderService testOrderService;

        public TestOrdersController(IDonorTestOrderService donorTestOrderService, ITestOrderService testOrderService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.donorTestOrderService = donorTestOrderService;
            this.testOrderService = testOrderService;
        }

        [HttpGet("blood-testtypes")]
        public async Task<ActionResult<RequestResult<List<BloodTestTypeDto>>>> GetBloodTestTypes()
        {
            try
            {
                var result = await this.donorTestOrderService.GetBloodTestTypes();
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving type of blood test: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("donor-testorders")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<DonorTestOrderListDto>>>> GetDonorTestOrders([FromBody] DonorTestOrderPagedSearchDto dto)
        {
            try
            {
                var results = await this.donorTestOrderService.GetDonorTestOrders(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving donor test orders: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("requested-testorders")]
        public async Task<ActionResult<RequestResult<PagedSearchResultDto<RequestedTestOrderListDto>>>> GetRequisitionTestOrders([FromBody] RequestedTestOrderPagedSearchDto dto)
        {
            try
            {
                var results = await this.testOrderService.SearchTestOrders(dto);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving blood request test orders: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("donor-testorder/{id}")]
        public async Task<ActionResult<RequestResult<DonorTestOrderDto>>> GetDonorTestOrder(Guid id)
        {
            try
            {
                var result = await this.donorTestOrderService.GetDonorTestOrder(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving donor test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving donor test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("create-donor-testorder")]
        public async Task<ActionResult<RequestResult<Guid>>> CreateDonorTestOrder([FromBody] CreateDonorTestOrderDto dto)
        {
            try
            {
                var result = await this.donorTestOrderService.CreateDonorTestOrder(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong creating donor test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong creating donor test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("update-donor-testorder")]
        public async Task<ActionResult<RequestResult<bool>>> UpdateDonorTestOrder([FromBody] DonorTestOrderDto dto)
        {
            try
            {
                var result = await this.donorTestOrderService.UpdateDonorTestOrder(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating donor test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong updating donor test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("create-testorder")]
        [Authorize(Policy = ApplicationRoles.TestOrderPolicy)]
        public async Task<ActionResult<RequestResult<Guid>>> CreateTestOrder([FromBody] CreateTestOrderDto dto)
        {
            try
            {
                var result = await this.testOrderService.CreateTestOrder(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong creating test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong creating test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("update-testorder")]
        [Authorize(Policy = ApplicationRoles.TestOrderPolicy)]
        public async Task<ActionResult<RequestResult<bool>>> UpdateTestOrder([FromBody] UpdateTestOrderDto dto)
        {
            try
            {
                var result = await this.testOrderService.UpdateTestOrder(dto, this.UserId);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong updating test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong updating test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("get-testorder/{id}")]
        public async Task<ActionResult<RequestResult<TestOrderDto>>> GetTestOrder(Guid id)
        {
            try
            {
                var result = await this.testOrderService.GetTestOrder(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("get-testorderstatus/{id}")]
        public async Task<ActionResult<RequestResult<bool>>> GetTestOrderStatus(Guid id)
        {
            try
            {
                var result = await this.testOrderService.GetTestOrderStatus(id);
                return this.Json(result);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving test order status: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving test order status: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("testordertypes")]
        public async Task<ActionResult<RequestResult<List<TestOrderTypeDto>>>> GetTestOrderTypes()
        {
            try
            {
                var result = await this.testOrderService.GetTestOrderTypes();
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving type of test orders: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("delete-testorder/{id}")]
        [Authorize(Policy = ApplicationRoles.TestOrderPolicy)]
        public async Task<ActionResult<RequestResult>> Delete(Guid id)
        {
            try
            {
                await this.testOrderService.DeleteTestOrder(id);
                return this.Json();
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong deleting user test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong deleting user test order: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("search-donors")]
        public async Task<ActionResult<RequestResult<List<TypeAheadResultDto>>>> SearchDonors(string st, string bt)
        {
            try
            {
                var result = await this.testOrderService.SearchDonors(st, bt);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong searching donors: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("search-patients")]
        public async Task<ActionResult<RequestResult<List<TypeAheadResultDto>>>> SearchPatients(string st)
        {
            try
            {
                var result = await this.testOrderService.SearchPatients(st);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong searching patients: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
               
    }
}
