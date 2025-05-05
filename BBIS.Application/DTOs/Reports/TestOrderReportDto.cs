using BBIS.Application.DTOs.TestOrder;

namespace BBIS.Application.DTOs.Reports
{
    public class TestOrderReportDto
    {
        public string InstitutionName { get; set; }
        public string TestOrderNumber { get; set; }
        public string TestCompleted { get; set; }
        public string DateCreated { get; set; }
        public string PatientName { get; set; }
        public string BloodType { get; set; }
        public string PatientIdNo { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Remarks { get; set; }
        
        public SignatoryReportDto PerformedBy { get; set; }
        public SignatoryReportDto ReviewedBy { get; set; }
        public SignatoryReportDto ValidatedBy { get; set; }
        public SignatoryReportDto Phatologist { get; set; }

        public List<CrossMatchTestOrderDto> CrossMatchTestOrders { get; set; }
        public CoombsTestOrderDto CoombsTestOrder { get; set; }
        public BloodScreeningTestOrderDto BloodScreeningTestOrder { get; set; }
        public BloodTypingTestOrderDto BloodTypingTestOrder { get; set; }
    }

    public class SignatoryReportDto
    {
        public string Name { get; set; }
        public string LicenseNo { get; set; }
    }
}
