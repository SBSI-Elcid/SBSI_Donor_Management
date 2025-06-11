namespace BBIS.Application.DTOs.DonorRegistration
{
    public class RegisterDonorDto : DonorDto
    {
        public Guid? ScheduleId { get; set; }
        public List<DonorMedicalQuestionnaireDto> MedicalHistories { get; set; } = new List<DonorMedicalQuestionnaireDto>();
    }
}
