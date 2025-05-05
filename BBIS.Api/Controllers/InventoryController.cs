using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Inventory;
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
    public class InventoryController : ApiControllerBase
    {
        private readonly IInventoryService inventoryService;
        private readonly IPrintReportService printReportService;

        public InventoryController(IInventoryService inventoryService, IPrintReportService printReportService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.inventoryService = inventoryService;
            this.printReportService = printReportService;
        }

        [HttpPost("addToInventory")]
        public async Task<ActionResult<RequestResult>> AddToInventory([FromBody] AddInventoryDto dto)
        {
            try
            {
                await this.inventoryService.AddToInventory(dto, this.UserId);
                return this.Json();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError($"Something went wrong adding inventory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (RecordExistException ex)
            {
                logger.LogError($"Something went wrong adding inventory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong adding inventory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong adding inventory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("prepareInventoryItems/{id}")]
        public async Task<ActionResult<RequestResult<List<InventoryItemDto>>>> PrepareInventoryItems(Guid id)
        {
            try
            {
                var results = await this.inventoryService.PrepareInventoryItems(id);
                return this.Json(results);
            }
            catch (RecordNotFoundException ex)
            {
                logger.LogError($"Something went wrong retrieving donor transaction: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving donor transaction: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("status")]
        public async Task<ActionResult<RequestResult<List<InventoryItemDto>>>> GetInvetoryStatus()
        {
            try
            {
                var results = await this.inventoryService.GetInventoryStatus();
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving inventory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("get-items")]
        public async Task<ActionResult<RequestResult<List<InventoryListDto>>>> GetInventoryItems([FromBody] InventoryFilterDto dto)
        {
            try
            {
                var results = await this.inventoryService.GetInventoryItems(dto);
                return this.Json(results);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError($"Something went wrong retrieving inventory items: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving inventory items: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("export-inventory")]
        public async Task<ActionResult> GetBloodComponentInventoryReportJson([FromBody] InventoryFilterDto filtersDto)
        {
            try
            {
                var reportData = await this.inventoryService.GetInventoryItemsForExport(filtersDto);

                var file = printReportService.GenerateExcelReport(reportData, "Blood_Component_Report");
                var fileName = $"Inventory_{DateTime.UtcNow.ToString("dd-MMM-yyyy")}.xlsx";

                Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"; filename*=UTF-8''{fileName}");
                Response.Headers.Add("X-Content-Type-Options", "nosniff");

                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong exporting inventory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("availableUnits/{bloodType}/{rh}")]
        public async Task<ActionResult<RequestResult<List<AvailableInventoryItemDto>>>> GetAvailableInventoryUnits(string bloodType, string rh)
        {
            try
            {
                var results = await this.inventoryService.GetAvailableInventoryUnits(bloodType, rh);
                return this.Json(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving inventory: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
