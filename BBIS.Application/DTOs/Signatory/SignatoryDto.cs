namespace BBIS.Application.DTOs.Signatory
{
    public class SignatoryDto
    {
        public Guid? SignatoryId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PositionPrefix { get; set; }
        public string LicenseNo { get; set; }
        public bool IsPathologist { get; set; }
        public bool IsActive { get; set; }
    }
}
