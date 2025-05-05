namespace BBIS.Domain.Models
{
    public class ReservationChecklist
    {
        public Guid ReservationChecklistId { get; set; }
        public Guid BloodComponentChecklistId { get; set; }
        public Guid ReservationItemId { get; set; }
      
        public virtual ReservationItem ReservationItem { get; set; }
        public virtual BloodComponentChecklist BloodComponentChecklist { get; set; }
    }
}
