namespace BBIS.Application.DTOs.ApplicationSetting
{
    public class InstitutionDto
    {
        public Guid? InstitutionId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
