namespace BBIS.Application.DTOs.Requisition
{
    public class CheckListOptionDto
    {
        public Guid BloodComponentId { get; set; }
        public string ComponentName { get; set; }
        public List<CheckListDto> CheckLists { get; set; }
    }

    public class CheckListDto
    {
        public Guid ChecklistId { get; set; }
        public string Description { get; set; }
        public bool IsAdult { get; set; }
    }
}
