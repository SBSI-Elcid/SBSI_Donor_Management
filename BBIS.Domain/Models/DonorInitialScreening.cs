using BBIS.Common.Enums;

namespace BBIS.Domain.Models
{
    public class DonorInitialScreening
    {
        public Guid DonorInitialScreeningId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public string MethodOfBloodCollection { get; set; }
        public double HGB { get; set; }
        public double HCT { get; set; }
        public double RBC { get; set; }
        public double WBC { get; set; }
        public double PLTCount { get; set; }
        //public string BloodType { get; set; }
        //public string DonationType { get; set; }
        //public string InHouseTypeValue { get; set; }

        //public string MobileBloodDonationPlace { get; set; }
        //public string MobileBloodDonationOrganizer { get; set; }

        //public string NameOfPatient { get; set; }
        //public string PatientHospital { get; set; }
        //public string PatientBloodType { get; set; }
        //public string PatientWBOrComponent { get; set; }
        //public double PatientNoOfUnits { get; set; }

        //public string PrcOffice { get; set; }

        public Guid ScreenById { get; set; } // User's Id who's doing the initial screening
        public DateTime ScreenDate { get; set; }

        public virtual DonorTransaction DonorTransaction { get; set; }
    }
}
