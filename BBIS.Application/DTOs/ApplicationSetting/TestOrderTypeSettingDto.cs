namespace BBIS.Application.DTOs.ApplicationSetting
{
    public class TestOrderTypeSettingDto
    {
        public Guid? TestOrderTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
