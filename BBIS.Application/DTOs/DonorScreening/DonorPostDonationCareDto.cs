using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorPostDonationCareDto
    {
        public Guid? DonorPostDonationCareId { get; set; }
        public Guid DonorTransactionId { get; set; } 
        public string TypeOfReaction { get; set; }
        public string SeverityOfReaction { get; set; }
        public string ReactionManifestation { get; set; }
        public string ActionInterventions { get; set; }
        public string DoctorsNote { get; set; }
        public string DischargeStatus { get; set; }
        public int PostDonationMonitoringId { get; set; }
        public Guid MonitoredBy { get; set; }
        public string DoctorName { get; set; }
        public DateTime DischargeDate { get; set; }

        public string DonorStatus { get; set; }

        public string DeferralStatus { get; set; }
        public string Remarks { get; set; }

        public List<PostDonationDetailsDto> PostDonationListDetails { get; set; }
        public List<VitalSignsMonitoringDto> VitalSignsMonitoringDetails { get; set; }
    }
}
