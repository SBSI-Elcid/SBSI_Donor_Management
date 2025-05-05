namespace BBIS.Application.DTOs.TestOrder
{
    public class RequestedTestOrderDto
    {
        public Guid TestOrderTypeId { get; set; }
        public string TestOrderName { get; set; }
        public string Result { get; set; }
    }
}
