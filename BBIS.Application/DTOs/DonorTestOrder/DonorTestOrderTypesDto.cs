namespace BBIS.Application.DTOs.DonorTestOrder
{
    public class DonorTestOrderTypesDto
    {
        public Guid DonorTestOrderTestTypeId { get; set; }
        public Guid BloodTestTypeId { get; set; }
        public string TestTypeName { get; set; }
        public bool IsReactive { get; set; }
        public string Remarks { get; set; }
    }
}
