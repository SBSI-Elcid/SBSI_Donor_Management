namespace BBIS.Application.DTOs.Signatory
{
    public class SignatoryViewDto 
    {
        public Guid SignatoryId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string PositionPrefix { get; set; }
        public string LicenseNo { get; set; }
        public bool IsPathologist { get; set; }
        public bool IsActive { get; set; }
    }

}
