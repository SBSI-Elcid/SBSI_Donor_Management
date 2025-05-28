using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class VitalSignsMonitoringDto
    {
        public int VitalSignsMonitoringId { get; set; } 

        public Guid? DonorPostDonationCareId { get; set; }
        public DateTime? Time { get; set; }
        public string BP { get; set; }
        public string PR { get; set; }

        public string Others { get; set; }
    }
}
