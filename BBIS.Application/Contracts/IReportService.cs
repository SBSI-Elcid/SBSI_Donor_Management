using BBIS.Application.DTOs.Reports;

namespace BBIS.Application.Contracts
{
    public interface IReportService
    {
        Task<List<DonorReportDto>> GetDonorReport(DonorReportFiltersDto filters);
        Task<ExportReportDto<DonorReportDto>> GetDonorReportForExport(DonorReportFiltersDto filters);
        Task<TestOrderReportDto> GetTestOrderReport(Guid id);
    }
}
