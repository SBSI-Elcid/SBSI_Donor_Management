namespace BBIS.Domain.Models
{
    public class Donor
    {
        public Guid DonorId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
              
        public DateTime BirthDate { get; set; }
        public string CivilStatus { get; set; }
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

        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public virtual ICollection<DonorRecentDonation> DonorRecentDonations { get; set; }
        public virtual ICollection<DonorTransaction> DonorTransactions { get; set; }      
    }
}
