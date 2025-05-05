namespace BBIS.Application.DTOs.Signatory
{
    public class SignatoryOptionDto
    {
        public Guid SignatoryId { get; set; }
        public string Name { get; set; }
        public bool IsPathologist { get; set; }
    }
}
