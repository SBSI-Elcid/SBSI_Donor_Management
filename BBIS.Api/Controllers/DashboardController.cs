using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Dashboard;
using BBIS.Application.DTOs.Schedule;
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
    public class DashboardController : ApiControllerBase
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("donorCount")]
        public async Task<ActionResult<RequestResult<int>>> GetDonorCount()
        {
            try
            {
                var results = await this.dashboardService.GetDonorCount();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving donor count: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("inventoryCount")]
        public async Task<ActionResult<RequestResult<InventoryCountDto>>> GetInventoryCount()
        {
            try
            {
                var results = await this.dashboardService.GetInventoryItemsCount();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving inventory count: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("bloodTypeCount")]
        public async Task<ActionResult<RequestResult<List<BloodTypeCountDto>>>> GetBloodTypeCount()
        {
            try
            {
                var results = await this.dashboardService.GetBloodTypeCount();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving blood type count: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("scheduleList")]
        public async Task<ActionResult<RequestResult<List<ScheduleDto>>>> GetSchedules()
        {
            try
            {
                var results = await this.dashboardService.GetSchedules();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving schedules: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("monthlyDonors")]
        public async Task<ActionResult<RequestResult<List<BloodTypeCountDto>>>> GetMonthlyDonorCount()
        {
            try
            {
                var results = await this.dashboardService.GetMonthlyDonorCount();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving monthly donors count: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
