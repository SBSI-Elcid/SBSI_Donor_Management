namespace BBIS.Application.DTOs.ApplicationSetting
{
    public class BloodComponentSettingDto
    {
        public Guid? BloodComponentId { get; set; }
        public string ComponentName { get; set; }
        public int ExpiryInDays { get; set; }
        public int NotifyOnDaysBefore { get; set; }
        public bool IsActive { get; set; }
    }
}
