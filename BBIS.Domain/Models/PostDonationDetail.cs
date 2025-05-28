using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Domain.Models
{
    public class PostDonationDetail
    {
        public int PostDonationDetailsId { get; set; } // Primary Key

        public Guid DonorPostDonationCareId { get; set; } // Foreign Key
        public string Details { get; set; }
        public string MonitoredBy { get; set; }

        public string Others { get; set; }

        public virtual DonorPostDonationCare DonorPostDonationCare { get; set; }
    }
}
