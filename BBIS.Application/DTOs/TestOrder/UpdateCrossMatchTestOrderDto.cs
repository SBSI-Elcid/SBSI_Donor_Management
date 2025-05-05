namespace BBIS.Application.DTOs.TestOrder
{
    public class UpdateCrossMatchTestOrderDto
    {
        public Guid CrossMathTestOrderId { get; set; }
        public string CrossMatchType { get; set; }
        public string Result { get; set; }
        public string LISS_AGH { get; set; }
    }
}
