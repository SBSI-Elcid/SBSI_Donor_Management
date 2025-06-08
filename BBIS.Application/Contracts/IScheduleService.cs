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
        Task<PagedSearchResultDto<ActivityDonorDto>> GetActivityDonor(ActivityDonorDto searchDto, Guid id);

        Task<ScheduleDto> GetScheduleById(Guid transactionId);

        Task<ChecklistDto> GetCheckList(Guid transactionId);

        Task<Guid> CreateUpdateCheckList(ChecklistDto dto, Guid userId);

        Task<Guid> CreateUpdateActivityDonor(ActivityDonorDto dto, Guid userId);

        Task<Guid> CreateUpdateSchedule(ScheduleDto dto, Guid userId);
    }
}
