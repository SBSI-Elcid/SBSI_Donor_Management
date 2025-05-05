using BBIS.Application.DTOs.Reports;
using BBIS.Domain.Models;

namespace BBIS.Application.Extension
{
    public static class SignatoryExtension
    {
        public static SignatoryReportDto GetSignatoryReportDto(this Signatory signatory)
        {
            return new SignatoryReportDto
            {
                Name = $"{signatory.FirstName} {signatory.MiddleName.Substring(0, 1)}. {signatory.LastName} - {signatory.PositionPrefix}",
                LicenseNo = signatory.LicenseNo
            };
        }
    }
}
