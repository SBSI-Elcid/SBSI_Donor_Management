namespace BBIS.Application.DTOs.DonorRegistration
{
    public class RegisteredDonorDto
    {
        public Guid DonorRegistrationId { get; set; }
        public string RegistrationNumber { get; set; }
        public string FullName { get; set; }
        public string CivilStatus { get; set; }
        public string Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
