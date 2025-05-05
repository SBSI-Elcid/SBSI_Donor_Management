namespace BBIS.Application.DTOs.ApplicationSetting
{
    public class BloodComponentChecklistListDto
    {
        public Guid? BloodComponentChecklistId { get; set; }
        public string BloodComponentName { get; set; }
        public string ChecklistDescription { get; set; }
        public bool IsAdult { get; set; }
        public bool IsActive { get; set; }
    }
}
