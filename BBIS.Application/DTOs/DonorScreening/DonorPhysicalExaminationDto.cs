namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorPhysicalExaminationDto
    {
        public Guid? DonorPhysicalExaminationId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public Guid DonorRegistrationId { get; set; }
        public string BloodPressure { get; set; }
        public double Pulse { get; set; }
        public double Temperature { get; set; }
        public string GeneralStatus { get; set; }
        public string Skin { get; set; }
        public string HEENT { get; set; }
        public string HeartAndLungs { get; set; }
        public string ResultStatus { get; set; }
        public string FailedRemarks { get; set; }
        public string BloodBagType { get; set; }

        public string DoctorName { get; set; }
        public Guid FacilitatedById { get; set; }
        public DateTime DateOfExamination { get; set; }

        public string DonorStatus { get; set; } // Status of the Donor application (e.g. 'For Initial Screening', 'For Physical Exam', 'Success', 'Deferred', etc)
        public string DeferralStatus { get; set; } // Whether the Donor was marked as temporary or permanent deferred by Screener
        public string Remarks { get; set; }
    }
}
