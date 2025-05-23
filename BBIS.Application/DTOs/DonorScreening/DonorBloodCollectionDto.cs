namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorBloodCollectionDto
    {
        public Guid? DonorBloodCollectionId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public Guid DonorRegistrationId { get; set; }
        //public string CollectionType { get; set; }
        //public string CollectionSubType { get; set; }

        public double CollectedBloodAmount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public bool Success { get; set; }
        public string UnwellReason { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMiddleName { get; set; }
        //public string MedicationGiven { get; set; }

        public Guid FacilitatedById { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PRCBloodDonorNumber { get; set; }


        public string SegmentSerialNumber { get; set; }
        public string DonorStatus { get; set; } // Status of the Donor application (e.g. 'For Initial Screening', 'For Physical Exam', 'Success', 'Deferred', etc)
        public string DeferralStatus { get; set; } // Whether the Donor was marked as temporary or permanent deferred by Screener
        public string Remarks { get; set; }
    }
}
