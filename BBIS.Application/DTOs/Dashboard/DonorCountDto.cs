namespace BBIS.Application.DTOs.Dashboard
{
    public class DonorCountDto
    {
        public List<MonthlyDonorCountDto> Donors { get; set; }
        public List<MonthlyDonorCountDto> DeferredDonors { get; set; }
    }
}
