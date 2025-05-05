using System.ComponentModel.DataAnnotations;

namespace BBIS.Application.DTOs.DonorRegistration
{
    public class VerifyDonorDto
    {
        [Required]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [Required]
        public string Lastname { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
