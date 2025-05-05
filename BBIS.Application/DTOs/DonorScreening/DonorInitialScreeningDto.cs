namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorInitialScreeningDto
    {
        public Guid? DonorInitialScreeningId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public Guid DonorRegistrationId { get; set; }

        public double Weight { get; set; } // in Kgs
        public double SPGR { get; set; }
        public double HGB { get; set; }
        public double HCT { get; set; }
        public double RBC { get; set; }
        public double WBC { get; set; }
        public double PLTCount { get; set; }
        public string BloodType { get; set; }
        public string DonationType { get; set; }
        public string InHouseTypeValue { get; set; }

        public string MobileBloodDonationPlace { get; set; }
        public string MobileBloodDonationOrganizer { get; set; }

        public string NameOfPatient { get; set; }
        public string PatientHospital { get; set; }
        public string PatientBloodType { get; set; }
        public string PatientWBOrComponent { get; set; }
        public double PatientNoOfUnits { get; set; }

        public string PrcOffice { get; set; }
        public Guid ScreenById { get; set; }

        public bool  HasRecentDonations { get; set; }
        public List<DonorRecentDonationDto> RecentDonations { get; set; }

        public string DonorStatus { get; set; } // Status of the Donor application (e.g. 'For Initial Screening', 'For Physical Exam', 'Success', 'Deferred', etc)
        public string DeferralStatus { get; set; } // Whether the Donor was marked as temporary or permanent deferred by Screener
        public string Remarks { get; set; }

        public string DonorName { get; set; }
        public bool HasTestOrder { get; set; }
        public bool TestCompleted { get; set; }
    }
}
