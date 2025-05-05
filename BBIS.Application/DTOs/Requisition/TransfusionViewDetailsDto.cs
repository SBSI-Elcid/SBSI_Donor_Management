namespace BBIS.Application.DTOs.Requisition
{
    public class TransfusionViewDetailsDto
    {
        public Guid ReservationId { get; set; }
        public string ReservationStatus { get; set; }
        public PatientDetailsDto PatientDetails { get; set; }
        public List<BloodComponentDetailsDto> BloodComponentDetails { get; set; }
    }

    public class PatientDetailsDto 
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
        public string PatientNo { get; set; }
        public string RequestingPhysician { get; set; }
        public string RoomNo { get; set; }
        public string Membership { get; set; }
        public DateTime? PreviousTransfusionDate { get; set; }
        public int? PreviousNoOfUnitsTransfused { get; set; }
    }

    public class BloodComponentDetailsDto
    {
        public Guid ReservationItemId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentBloodType { get; set; }
        public string ComponentBloodRh { get; set; }
        public double Volume { get; set; }
        public string UnitMeasure { get; set; }
        public string UnitSerialNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TransfusionDto Tranfusion { get; set; }
    }

    public class TransfusionViewDto
    {
        public Guid? TransfusionId { get; set; }
        public DateTime? TransfusionStarted { get; set; }
        public DateTime? TransfusionEnded { get; set; }
        public double TotalTimeConsumed { get; set; }
        public string MedicationGiven { get; set; }
        public string HookedBy { get; set; }
        public string RemovedBy { get; set; }
        public List<TransfusionVitalSignDto> VitalSigns { get; set; }
        public string TransfusionStatus { get; set; }
        public string TransfusionNotes { get; set; }
    }

    public class TransfusionVitalSignDto
    {
        public Guid? VitalSignId { get; set; }
        public string VitalSignType { get; set; }
        public double? Temperature { get; set; }
        public string BloodPressure { get; set; }
        public double? RespiratoryRate { get; set; }
        public double? PulseRate { get; set; }
        public string Remarks { get; set; }
    }
}
