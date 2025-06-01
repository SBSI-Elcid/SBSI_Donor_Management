using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Application.DTOs.Schedule;

namespace BBIS.Application.Contracts
{
    public interface IScheduleService
    {
        Task<PagedSearchResultDto<ScheduleDto>> GetSchedules(ScheduleDto searchDto);

        Task<ScheduleDto> GetScheduleById(Guid transactionId);

        Task<Guid> CreateUpdateSchedule(ScheduleDto dto, Guid userId);
    }
}
