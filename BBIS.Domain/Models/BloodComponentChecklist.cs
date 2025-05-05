namespace BBIS.Domain.Models
{
    public class BloodComponentChecklist
    {
        public Guid BloodComponentChecklistId { get; set; }
        public Guid BloodComponentId { get; set; }
        public string ChecklistDescription { get; set; }
        public bool IsAdult { get; set; }
        public bool IsActive { get; set; }

        public virtual BloodComponent BloodComponent { get; set; }
        public virtual ICollection<ReservationChecklist> ReservationChecklists { get; set; }
    }
}
