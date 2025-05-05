using BBIS.Application.DTOs.DonorRegistration;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorListDto : RegisteredDonorDto
    {
        public Guid DonorTransactionId { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNo { get; set; }
        public string BloodType { get; set; }
        public string DonorStatus { get; set; }
        public bool HasTestOrder { get; set; }
        public bool TestOrderCompleted { get; set; }
        public bool IsOffline { get; set; }
        public bool IsSync { get; set; }
    }
}