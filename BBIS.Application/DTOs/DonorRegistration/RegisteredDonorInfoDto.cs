namespace BBIS.Application.DTOs.DonorRegistration
{
    public class RegisteredDonorInfoDto : DonorDto
    {
        public Guid DonorRegistrationId { get; set; }
        public string RegistrationNumber { get; set; }
        public string DonorStatus { get; set; }
        public Guid? DonorTransactionId { get; set; }

        public List<DonorMedicalHistoryViewDto> MedicalHistories { get; set; } = new List<DonorMedicalHistoryViewDto>();
    }
}
