using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Application.DTOs.Common;

namespace BBIS.Application.DTOs.Schedule
{
    public class ActivityDonorDto : PagedSearchDto
    {
        public int? ActivityDonorId { get; set; }
        public Guid ScheduleId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string FullName { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
