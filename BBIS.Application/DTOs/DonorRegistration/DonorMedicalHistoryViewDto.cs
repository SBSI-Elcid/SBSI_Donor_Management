namespace BBIS.Application.DTOs.DonorRegistration
{
    public class DonorMedicalHistoryViewDto
    {
        public int MedicalQuestionnaireId { get; set; }
        public string HeaderText { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public string Remarks { get; set; }
    }
}
