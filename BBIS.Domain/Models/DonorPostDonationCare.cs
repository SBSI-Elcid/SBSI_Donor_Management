using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Domain.Models
{
    public class DonorPostDonationCare
    {
        public Guid DonorPostDonationCareId { get; set; } // Primary Key

        public Guid DonorTransactionId { get; set; } // Foreign Key
        public string TypeOfReaction { get; set; }
        public string SeverityOfReaction { get; set; }
        public string ReactionManifestation { get; set; }
        public string ActionInterventions { get; set; }
        //public int VitalSignsMonitoringId { get; set; }
        public string DoctorsNote { get; set; }
        public string DischargeStatus   { get; set ; }
        public int PostDonationMonitoringId { get; set; }
        public Guid MonitoredBy { get; set; }
        public string DoctorName { get; set; }
        public DateTime DischargeDate { get; set; }

        public virtual DonorTransaction DonorTransaction { get; set; }
        public ICollection<VitalSignsMonitoring> VitalSignsMonitorings { get; set; }
    }
}
