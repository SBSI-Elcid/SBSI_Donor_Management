namespace BBIS.Domain.Models
{
    public class MedicalQuestionnaire
    {
        public int MedicalQuestionnaireId { get; set; }
        public string HeaderText { get; set; }
        public string QuestionTagalogText { get; set; }
        public string QuestionEnglishText { get; set; }
        public string QuestionBisayaText { get; set; }
        public int OrderNo { get; set; }
        public string GenderOption { get; set; }

        public virtual ICollection<DonorMedicalHistory> DonorMedicalHistories { get; set; }
    }
}
