using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorVitalSignsDto
    {
        public Guid? DonorVitalSignsId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public Guid DonorRegistrationId { get; set; }
        public double BodyWeight { get; set; }
        public double Temperature { get; set; }
        public string BloodPressure { get; set; }
        public double PulseRate { get; set; }
        public double RespiratoryRate { get; set; }
        public double OxygenSaturation { get; set; }
        public Guid FacilitatedBy { get; set; }
        public DateTime DateOfExamination { get; set; }
        public string DonorName { get; set; }
    }
}
