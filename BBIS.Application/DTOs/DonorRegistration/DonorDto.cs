using System.ComponentModel.DataAnnotations;

namespace BBIS.Application.DTOs.DonorRegistration
{
    public class DonorDto
    {
        [Required(ErrorMessage = "Last name is required.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Civil status is required")]
        public string CivilStatus { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public string AddressNo { get; set; }
        public string AddressStreet { get; set; }
        public string AddressBarangay { get; set; }
        public string AddressMunicipality { get; set; }
        public string AddressProvinceOrCity { get; set; }
        public string AddressZipcode { get; set; }

        public string OfficeAddress { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Education { get; set; }
        public string WorkName { get; set; }

        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }

        public string SchoolIdNo { get; set; }
        public string CompanyIdNo { get; set; }
        public string PRCNo { get; set; }
        public string DriverLicenseNo { get; set; }
        public string SssGsisBirNo { get; set; }
        public string OtherNo { get; set; }
    }
}
