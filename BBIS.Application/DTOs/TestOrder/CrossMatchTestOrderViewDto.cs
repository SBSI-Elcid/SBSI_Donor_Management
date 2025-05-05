namespace BBIS.Application.DTOs.TestOrder
{
    public class CrossMatchTestOrderViewDto
    {
        public Guid CrossMatchTestOrderId { get; set; }
        public string DonorUnitSerialNumber { get; set; }
        public string CrossMatchType { get; set; }
        public string Result { get; set; }
    }
}
