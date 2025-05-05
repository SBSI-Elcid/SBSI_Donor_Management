namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorRecentDonationDto
    {
        public Guid? DonorRecentDonationId { get; set; }
        public Guid DonorId { get; set; }
        public int NumberOfDonation { get; set; }
        public DateTime DateOfRecentDonation { get; set; }
        public string PlaceOfRecentDonation { get; set; }
        public string Agency { get; set; }
    }
}
