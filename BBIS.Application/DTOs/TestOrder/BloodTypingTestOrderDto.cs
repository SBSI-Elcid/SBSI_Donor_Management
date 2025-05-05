namespace BBIS.Application.DTOs.TestOrder
{
    public class BloodTypingTestOrderDto
    {
        public Guid BloodTypingTestOrderId { get; set; }
        // Forward Typing
        public string FTAntiA { get; set; }
        public string FTAntiB { get; set; }
        public string FTAntiAB { get; set; }
        public string FTAntiD { get; set; }
        public string FTAntiD2 { get; set; }
        //Reverse Typing
        public string RTACells { get; set; }
        public string RTBCells { get; set; }
        public string Control { get; set; }
        public string BloodType { get; set; }
        public string BloodRh { get; set; }
    }


}
