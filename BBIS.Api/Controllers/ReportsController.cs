using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Reports;
using BBIS.Common;
using BBIS.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spire.Barcode;
using System.Drawing;
using System.Net;
using System.Net.Mime;

namespace BBIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ApiControllerBase
    {
        private readonly IReportService reportService;
        private readonly IPrintReportService printReportService;

        public ReportsController(IReportService reportService, IPrintReportService printReportService, ILoggerManager logger, IConfiguration configuration) : base(logger, configuration)
        {
            this.reportService = reportService;
            this.printReportService = printReportService;
        }

        [HttpPost("report-donor")]
        public async Task<ActionResult<RequestResult<List<DonorReportDto>>>> GetDonorReport([FromBody] DonorReportFiltersDto filtersDto)
        {
            try
            {
                var result = await this.reportService.GetDonorReport(filtersDto);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving donor report: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("export-donor")]
        public async Task<ActionResult> ExportDonorReport([FromBody] DonorReportFiltersDto filtersDto)
        {
            try
            {
                var reportData = await this.reportService.GetDonorReportForExport(filtersDto);

                var file = printReportService.GenerateExcelReport(reportData, "Donor_Report");
                var fileName = $"Donor_Report_{DateTime.UtcNow.ToString("dd-MMM-yyyy")}.xlsx";

                Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"; filename*=UTF-8''{fileName}");
                Response.Headers.Add("X-Content-Type-Options", "nosniff");

                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong exporting donor report: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("testorder-report/{id}")]
        public async Task<ActionResult<RequestResult<TestOrderReportDto>>> GetTestOrderReport([FromRoute] Guid id)
        {
            try
            {
                var result = await this.reportService.GetTestOrderReport(id);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong retrieving test order report: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("generate-barcode")]
        public async Task<ActionResult<RequestResult<List<BarcodeResultDto>>>> GenerateBarcode([FromBody] GenerateBarcodeDto dto)
        {
            try
            {
                var result = GenerateBarcodeInternal(dto);
                return this.Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong generating barcode: {ex.Message}");
                return this.JsonError(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private List<BarcodeResultDto> GenerateBarcodeInternal(GenerateBarcodeDto dto)
        {
            var results = new List<BarcodeResultDto>();

            foreach (var item in dto.Items)
            {
                var result = new BarcodeResultDto() { BarcodeText = item.BarcodeText };

                BarcodeSettings bs = new BarcodeSettings();

                bs.Type = BarCodeType.Code128;
                bs.Data = item.BarcodeText;
                bs.BarHeight = 21;
                bs.AutoResize = true;
                bs.ShowTextOnBottom = true;
                bs.TextAlignment = StringAlignment.Near;
                bs.TextFont = new Font("sans-serif, arial", 8, FontStyle.Bold);
                bs.ShowText = true;
                bs.LeftMargin = 1;
                bs.RightMargin = 1;

                if (!string.IsNullOrEmpty(item.BottomText))
                {
                    bs.ShowBottomText = true;
                    bs.BottomText = item.BottomText;
                    bs.BottomTextAligment = StringAlignment.Far;
                    bs.BottomTextFont = new Font("sans-serif, arial", 7, FontStyle.Bold);
                }

                if (!string.IsNullOrEmpty(item.TopText))
                {
                    bs.ShowTopText = true;
                    bs.TopText = item.TopText;
                    bs.TopTextAligment = StringAlignment.Near;
                    bs.TopTextFont = new Font("sans-serif, arial", 7, FontStyle.Bold);
                }
             
                BarCodeGenerator bg = new BarCodeGenerator(bs);
                var image = bg.GenerateImage();
                
                ImageConverter _imageConverter = new ImageConverter();
                byte[] imageBytes = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));

                result.Barcode = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";

                results.Add(result);
            }

            return results;
        }

    }
}
