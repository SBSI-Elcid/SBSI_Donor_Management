namespace BBIS.Application.DTOs.Requisition
{
    public class ReservationItemDto
    {
        public Guid BloodComponentId { get; set; }
        public Guid InventoryItemId { get; set; }

        public string OtherNotes { get; set; }

        public List<Guid> ReservationCheckLists { get; set; }
    }
}
