namespace BBIS.Application.DTOs.DonorRegistration
{
    public class RegisterDonorDto : DonorDto
    {
        public List<DonorMedicalQuestionnaireDto> MedicalHistories { get; set; } = new List<DonorMedicalQuestionnaireDto>();
    }
}
