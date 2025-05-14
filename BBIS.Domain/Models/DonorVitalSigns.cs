using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBIS.Domain.Models
{
    public class DonorVitalSigns
    {
        public Guid DonorVitalSignsId { get; set; } // Primary Key

        public Guid DonorTransactionId { get; set; } // Foreign Key

        public double BodyWeight { get; set; }
        public double Temperature { get; set; }
        public string BloodPressure { get; set; } // varchar(10)
        public double PulseRate { get; set; }
        public double RespiratoryRate { get; set; }
        public double OxygenSaturation { get; set; }

        public Guid FacilitatedBy { get; set; }

        public DateTime DateOfExamination { get; set; }

        // Navigation property
        public virtual DonorTransaction DonorTransaction { get; set; }
    }
}
