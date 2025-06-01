using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Schedule;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;



namespace BBIS.Application.Services
{
    public class ScheduleServices : IScheduleService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public ScheduleServices(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PagedSearchResultDto<ScheduleDto>> GetSchedules(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(searchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy)
                ? "ScheduleDateTime asc"
                : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");

            var pagedResult = new PagedSearchResultDto<ScheduleDto>(searchDto);

            var query = dbContext.Schedules.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.SearchText))
            {
                query = query.Where(x =>
                    x.ActivityName.Contains(searchDto.SearchText) ||
                    x.ActivityType.Contains(searchDto.SearchText) ||
                    x.ActivityVenue.Contains(searchDto.SearchText) ||
                    x.PartnerInstitutionName.Contains(searchDto.SearchText) ||
                    x.PointPersonName.Contains(searchDto.SearchText));
            }

            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x => new ScheduleDto
                {
                    ScheduleId = x.ScheduleId,
                    Status = x.Status,
                    ActivityName = x.ActivityName,
                    ActivityType = x.ActivityType,
                    ScheduleDateTime = x.ScheduleDateTime,
                    ActivityVenue = x.ActivityVenue,
                    PartnerInstitutionName = x.PartnerInstitutionName,
                    PointPersonName = x.PointPersonName,
                    ExpectedDonorNumber = x.ExpectedDonorNumber,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                })
                .AsNoTracking()
                .ToListAsync();

            var totalRecords = query.Count();

            if (results == null || !results.Any()) return pagedResult;

            pagedResult.TotalCount = totalRecords;
            pagedResult.Results = results.AsQueryable()
                .OrderBy(x => x.ScheduleId)
                .ToList();

            return pagedResult;
        }
    }
    }
