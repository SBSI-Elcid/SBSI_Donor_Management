namespace BBIS.Domain.Models
{
    public class DonorBloodCollection
    {
        public Guid DonorBloodCollectionId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public string CollectionType { get; set; }
        public string CollectionSubType { get; set; }

        public double CollectedBloodAmount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public bool Success { get; set; }
        public string UnwellReason { get; set; }
        public string MedicationGiven { get; set; }

        public Guid FacilitatedById { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
              
        public virtual DonorTransaction DonorTransaction { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
