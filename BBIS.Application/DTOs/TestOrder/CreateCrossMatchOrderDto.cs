namespace BBIS.Application.DTOs.TestOrder
{
    public class CreateCrossMatchOrderDto
    {
        public Guid BloodComponentId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public string BloodComponentName { get; set; }
        public string DonorUnitSerialNumber { get; set; }
        public string CrossMatchType { get; set; }
        public string Result { get; set; }
        public string Source { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double Volume { get; set; }
    }

    public class GroupCrossMatchOrderDto
    {
        public Guid BloodComponentId { get; set; }
        public string BloodComponentName { get; set; }

        public List<CreateCrossMatchOrderDto> CrossMatchTestOrders { get; set; }
    }
}
