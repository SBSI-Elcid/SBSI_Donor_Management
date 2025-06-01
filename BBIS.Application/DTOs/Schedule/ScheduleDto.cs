using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Domain.Models;

namespace BBIS.Application.DTOs.Schedule
{
    public class ScheduleDto
    {
        public Guid ScheduleId { get; set; }
        public string Status { get; set; }
        public string ActivityName { get; set; }
        public string ActivityType { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public string ActivityVenue { get; set; }
        public string PartnerInstitutionName { get; set; }
        public string PointPersonName { get; set; }
        public int ExpectedDonorNumber { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
