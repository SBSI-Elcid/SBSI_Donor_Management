namespace BBIS.Domain.Models
{
    public class OtherTestOrder
    {
        public Guid OtherTestOrderId { get; set; }
        public Guid TestOrderTypeId { get; set; }
       // public Guid CrossMatchTestOrderId { get; set; }

        public string Result { get; set; } //Positive or Negative
        public string Remarks { get; set; }

       // public virtual CrossMatchTestOrder CrossMatchTestOrder { get; set; }
       // public virtual TestOrderType TestOrderType { get; set; }
    }
}
