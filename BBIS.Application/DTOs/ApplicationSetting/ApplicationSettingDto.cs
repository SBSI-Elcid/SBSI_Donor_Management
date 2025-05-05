namespace BBIS.Application.DTOs.ApplicationSetting
{
    public class ApplicationSettingDto : ApplicationSettingKeyValuePair
    {
        public Guid ApplicationSettingId { get; set; }
        public bool IsActive { get; set; }
    }
}
