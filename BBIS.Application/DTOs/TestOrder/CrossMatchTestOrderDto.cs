namespace BBIS.Application.DTOs.TestOrder
{
    public class CrossMatchTestOrderDto
    {
        public Guid CrossMatchTestOrderId { get; set; }
        public string BloodComponentName { get; set; }
        public Guid BloodComponentId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public string DonorUnitSerialNumber { get; set; }
        public string CrossMatchType { get; set; }
        public string Result { get; set; }
        public string Source { get; set; }
        public string LISS_AGH { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class GroupCrossMatchTestOrderViewDto
    {
        public string BloodComponentName { get; set; }
        public Guid BloodComponentId { get; set; }

        public List<CrossMatchTestOrderDto> CrossMatchTestOrders { get; set; }
    }
}
                                            