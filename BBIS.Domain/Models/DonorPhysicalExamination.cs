namespace BBIS.Domain.Models
{
    public class DonorPhysicalExamination
    {
        public Guid DonorPhysicalExaminationId { get; set; }
        public Guid DonorTransactionId { get; set; }
        //public string BloodPressure { get; set; }
        //public double Pulse { get; set; }
        //public double Temperature { get; set; }
        public string GeneralStatus { get; set; }
        public string Skin { get; set; }
        public string HEENT { get; set; }
        public string HeartAndLungs { get; set; }
      
        //public string ResultStatus { get; set; }
        //public string FailedRemarks { get; set; }

        //public string BloodBagType { get; set; }

        //public string DoctorName { get; set; }
        public Guid FacilitatedById { get; set; }

        public DateTime DateOfExamination { get; set; }

        public virtual DonorTransaction DonorTransaction { get; set; }
    }
}
