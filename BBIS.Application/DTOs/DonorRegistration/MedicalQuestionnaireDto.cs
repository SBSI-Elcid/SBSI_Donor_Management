namespace BBIS.Application.DTOs.DonorRegistration
{
    public class MedicalQuestionnaireDto
    {
        public int MedicalQuestionnaireId { get; set; }
        public string HeaderText { get; set; }
        public string QuestionTagalogText { get; set; }
        public string QuestionEnglishText { get; set; }
        public string QuestionBisayaText { get; set; }
        public int OrderNo { get; set; }
        public string GenderOption { get; set; }
    }
}
