using BBIS.Application.DTOs.Reports;

namespace BBIS.Application.Contracts
{
    public interface IPrintReportService
    {
        byte[] GenerateExcelReport<T>(ExportReportDto<T> reportData, string sheetName);
        public byte[] GeneratePdfReport(string html);
    }
}
