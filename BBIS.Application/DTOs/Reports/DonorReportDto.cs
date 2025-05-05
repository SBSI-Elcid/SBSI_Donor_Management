namespace BBIS.Application.DTOs.Reports
{
    public class DonorReportDto
    {
        public string UnitSerialNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public string CollectedAmount { get; set; }
        public DateTime DateOfDonation { get; set; }
        public string Status { get; set; }
    }
}
