namespace BBIS.Domain.Models
{
    public class DonorRecentDonation
    {
        public Guid DonorRecentDonationId { get; set; }
        public Guid DonorId { get; set; } // This will not be link to donortransaction
        public int NumberOfDonation { get; set; }
        public DateTime DateOfRecentDonation { get; set; }
        public string PlaceOfRecentDonation { get; set; }
        public string Agency { get; set; } // Hospital or Redcross

        public virtual Donor Donor { get; set; }
    }
}
