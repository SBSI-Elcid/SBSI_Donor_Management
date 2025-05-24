using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorBloodBagIssuanceDto
    {
        public Guid? DonorBloodBagIssuanceId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public Guid DonorRegistrationId { get; set; }

        public List<DonorBloodBagInfo> BloodBagInfos { get; set; } =  new List<DonorBloodBagInfo>();
        //public string BloodBagToBeUsed { get; set; }
        //public string BloodBagType { get; set; }
        //public string UnitSerialNumber { get; set; }
        //public string SegmentSerialNumber { get; set; }

        //public Boolean isFromModal { get; set; }
        //public string PatientFirstName { get; set; }
        //public string PatientLastName { get; set; }
        //public string PatientMiddleName { get; set; }
        public Guid IssuedBy { get; set; }
        public DateTime IssuedDate { get; set; }
        public string DonorStatus { get; set; }
    }
}
