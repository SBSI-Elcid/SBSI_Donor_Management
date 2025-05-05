namespace BBIS.Application.DTOs.Patient
{
    public class PatientResult
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
    }
}
