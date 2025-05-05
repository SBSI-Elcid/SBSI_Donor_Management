namespace BBIS.Domain.Models
{
    public class CoombsTestOrder
    {
        public Guid CoombsTestOrderId { get; set; }
        public Guid TestOrderId { get; set; }
        public string DATResult { get; set; }
        public string IATResult { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual TestOrder TestOrder { get; set; }
    }
}
