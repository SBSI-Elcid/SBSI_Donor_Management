namespace BBIS.Domain.Models
{
    public class BloodScreeningTestOrder
    {
        public Guid BloodScreeningTestOrderId { get; set; }
        public Guid TestOrderId { get; set; }
        public string Result { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual TestOrder TestOrder { get; set; }
    }
}
