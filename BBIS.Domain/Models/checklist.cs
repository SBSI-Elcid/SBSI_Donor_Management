using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Domain.Models
{
    public class checklist
    {
        public int ChecklistId { get; set; }
        public Guid ScheduleId  { get; set; }
        public string ConfirmationCall { get; set; }
        public string VerificationCall { get; set; }
        public string FinalCall { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public virtual Schedule Schedule { get; set; }

    }
}
