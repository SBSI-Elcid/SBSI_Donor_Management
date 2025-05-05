namespace BBIS.Domain.Models
{
    public class ApplicationSetting
    {
        public Guid ApplicationSettingId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public bool IsActive { get; set; }
    }
}
