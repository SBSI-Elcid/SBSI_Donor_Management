namespace BBIS.Domain.Models
{
    public class DonorTransaction
    {
        public Guid DonorTransactionId { get; set; }
        public Guid DonorRegistrationId { get; set; }
        public Guid DonorId { get; set; }
        public string PRCBloodDonorNumber { get; set; }
        public string DOHNBBNetsBarcode { get; set; }
        public DateTime? DateOfDonation { get; set; }
        public string UnitSerialNumber { get; set; }
        public bool BloodIsSafeToTransfuse { get; set; }
        public string DonorStatus { get; set; }

        // this will be populated from laboratory test
        public string FinalBloodType { get; set; }
        public string BloodRh { get; set; }

        public bool IsOffline { get; set; }
        public bool IsSync { get; set; }

        public virtual Donor Donor { get; set; }
        public virtual DonorRegistration DonorRegistration { get; set; }

        public virtual DonorInitialScreening DonorInitialScreening { get; set; }
        public virtual DonorPhysicalExamination DonorPhysicalExamination { get; set; }
        public virtual DonorBloodCollection DonorBloodCollection { get; set; }
        public virtual DonorDeferral DonorDeferral { get; set; }
        public virtual DonorTestOrder DonorTestOrder { get; set; }
        public virtual ICollection<InventorySource> InventorySources { get; set; }

        public virtual ICollection<ReservationItem> ReservationItems { get; set; }
        public virtual ICollection<CrossMatchTestOrder> CrossMatchTestOrders { get; set; }
    }
}
