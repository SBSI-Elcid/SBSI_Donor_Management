namespace BBIS.Application.DTOs.ApplicationSetting
{
    public class BloodComponentChecklistDto
    {
        public Guid? BloodComponentChecklistId { get; set; }
        public Guid BloodComponentId { get; set; }
        public string ChecklistDescription { get; set; }
        public bool IsAdult { get; set; }
        public bool IsActive { get; set; }
    }
}
