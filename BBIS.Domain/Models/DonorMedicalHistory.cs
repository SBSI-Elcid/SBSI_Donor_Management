namespace BBIS.Domain.Models
{
    public class DonorMedicalHistory
    {
        public Guid DonorMedicalHistoryId { get; set; }
        public Guid DonorRegistrationId { get; set; }
        public int MedicalQuestionnaireId { get; set; }
        public string Answer { get; set; }
        public string Remarks { get; set; }

        public virtual DonorRegistration DonorRegistration { get; set; }
        public virtual MedicalQuestionnaire MedicalQuestionnaire { get; set; }
    }
}
