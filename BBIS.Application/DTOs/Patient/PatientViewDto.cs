namespace BBIS.Application.DTOs.Patient
{
    public class PatientViewDto
    {
        public Guid PatientId { get; set; }
        public string BloodType { get; set; }
        public string Rh { get; set; }
        public string PatientIdNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
