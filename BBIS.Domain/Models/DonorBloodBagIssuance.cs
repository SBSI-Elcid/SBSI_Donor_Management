using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Domain.Models
{
    public class DonorBloodBagIssuance
    {
        public Guid DonorBloodBagIssuanceId { get; set; } // Primary Key

        public Guid DonorTransactionId { get; set; } // Foreign Key
        public string BloodBagToBeUsed { get; set; }
        public string BloodBagType { get; set; }
        public string UnitSerialNumber { get; set; }
        public string SegmentSerialNumber { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMiddleName { get; set; }
        public Guid IssuedBy { get; set; }
        public DateTime IssuedDate { get; set; }

        public virtual DonorTransaction DonorTransaction { get; set; }
    }
}
