namespace BBIS.Application.DTOs.Reports
{
    public class GenerateBarcodeDto
    {
        public List<BarcodeInfoDto> Items { get; set; }
    }

    public class BarcodeInfoDto
    {
        public string BarcodeText { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
    }
}
