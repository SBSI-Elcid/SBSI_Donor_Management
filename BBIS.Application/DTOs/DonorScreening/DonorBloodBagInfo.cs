using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorBloodBagInfo
    {
        public string BloodBagToBeUsed { get; set; }
        public string BloodBagType { get; set; }
        public string UnitSerialNumber { get; set; }
        public string SegmentSerialNumber { get; set; }

        public Boolean isFromModal { get; set; }
    }
}
