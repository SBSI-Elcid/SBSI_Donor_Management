using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Application.DTOs.DonorRegistration;

namespace BBIS.Application.DTOs.DonorScreening
{
    public class DonorCounselingDto
    {
        public Guid DonorMedicalHistoryID { get; set; }
        public Guid DonorRegistrationId { get; set; }
        public Guid DonorTransactionId { get; set; }
        public List<DonorMedicalQuestionnaireDto> MedicalHistories { get; set; } = new List<DonorMedicalQuestionnaireDto>();

        public string DonorStatus { get; set; }

       
    }
}

