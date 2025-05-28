using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Domain.Models;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class PostDonationDetailsDto
    {
        public int PostDonationDetailsId { get; set; } 

        public Guid DonorPostDonationCareId { get; set; } 
        public string Details { get; set; }
        public string MonitoredBy { get; set; }

    }
}
