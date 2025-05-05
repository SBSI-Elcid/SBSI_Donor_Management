namespace BBIS.Application.DTOs.Requisition
{
    public class ReservationDto
    {
       
        public Guid PatientId { get; set; }
        public string PatientType { get; set; }
        public string PriorityLevel { get; set; }
        public bool ForAdult { get; set; }
        public bool IsEmergency { get; set; }
        public string RoomNo { get; set; }
        public string Membership { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public DateTime? PreviousTransfusionDate { get; set; }
        public int? PreviousNoOfUnitsTransfused { get; set; }

        public List<ReservationItemDto> ReservationItems { get; set; }
    }
}
