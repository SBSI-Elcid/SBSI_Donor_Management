namespace BBIS.Domain.Models
{
    public class TransfusionVitalSign
    {
        public Guid VitalSignId { get; set; }
        public Guid TransfusionId { get; set; }
        public string VitalSignType { get; set; }
        public double? Temperature { get; set; }
        public string BloodPressure { get; set; }
        public double? RespiratoryRate { get; set; }
        public double? PulseRate { get; set; }
        public string Remarks { get; set; }

        public virtual Transfusion Transfusion { get; set; }
    }
}
