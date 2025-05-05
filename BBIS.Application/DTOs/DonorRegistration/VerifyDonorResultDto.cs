namespace BBIS.Application.DTOs.DonorRegistration
{
    public class VerifyDonorResultDto
    {
        public bool IsValid { get; set; }
        public string DeferralStatus { get; set; }
        public string Remarks { get; set; }
        public DonorDto Donor { get; set; }
    }
}
