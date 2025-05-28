using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Domain.Models
{
    public class VitalSignsMonitoring
    {
        public int VitalSignsMonitoringId { get; set; } // Primary Key

        public Guid DonorPostDonationCareId { get; set; } // Foreign Key
        public DateTime Time { get; set; }
        public string BP { get; set; }
        public string PR { get; set; }

        public string Others { get; set; }

        public virtual DonorPostDonationCare DonorPostDonationCare { get; set; }
    }
}
