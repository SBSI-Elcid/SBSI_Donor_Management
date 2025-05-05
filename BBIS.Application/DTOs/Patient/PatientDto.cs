namespace BBIS.Application.DTOs.Patient
{
    public class PatientDto
    {
        public Guid? PatientId { get; set; }    
        public string BloodType { get; set; }
        public string Rh { get; set; }
        public string PatientIdNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }
}
