namespace BBIS.Application.DTOs.DonorRegistration
{
    public class UpdateDonorInfoDto : DonorDto
    {
        public Guid DonorRegistrationId { get; set; }
        public List<DonorMedicalQuestionnaireDto> MedicalHistories { get; set; } = new List<DonorMedicalQuestionnaireDto>();
    }
}
