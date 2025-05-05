using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.ApplicationSetting;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Patient;
using BBIS.Common.Exceptions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;

namespace BBIS.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public PatientService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PatientResult> CreatePatient(PatientDto dto, Guid userId)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(PatientDto));
            }

            var newPatient = mapper.Map<Patient>(dto);
            newPatient.CreatedBy = userId;
            newPatient.CreatedOn = DateTime.Now;

            repository.Patient.Create(newPatient);

            await repository.SaveAsync();

            return new PatientResult 
            { 
                BloodRh = dto.Rh,
                BloodType = dto.BloodType,
                PatientId = newPatient.PatientId,
                PatientName = $"{dto.FirstName} {dto.MiddleName.Substring(0,1)}. {dto.LastName}"
            };
        }

        public async Task<PatientResult> UpdatePatient(PatientDto dto, Guid userId)
        {
            var patient = await repository.Patient.FindOneByConditionAsync(x => x.PatientId == dto.PatientId);

            if(patient == null)
            {
                throw new RecordNotFoundException($"Patient not found for Id: {dto.PatientId}");
            }

            patient = mapper.Map<Patient>(dto);
            patient.UpdatedBy = userId;
            patient.UpdatedDate = DateTime.UtcNow;

            repository.Patient.Update(patient);

            await repository.SaveAsync();

            return new PatientResult
            {
                BloodRh = dto.Rh,
                BloodType = dto.BloodType,
                PatientId = patient.PatientId,
                PatientName = $"{dto.FirstName} {dto.MiddleName.Substring(0, 1)}. {dto.LastName}"
            };
        }

        public async Task<PagedSearchResultDto<PatientViewDto>> GetPagedListAsync(PatientPagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "FullName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<PatientViewDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.Patients
                .Where(x => 
                    (!string.IsNullOrEmpty(searchDto.BloodType) && !string.IsNullOrEmpty(searchDto.BloodRh))
                    ? x.BloodType == searchDto.BloodType && x.Rh == searchDto.BloodRh
                    : true
                ).Search(t => t.FirstName, t => t.MiddleName, t => t.LastName)
                .Containing(searchText);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                    .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                    .Take(searchDto.PageSize)
                    .Select(x =>
                        new PatientViewDto
                        {
                            BloodType = x.BloodType,
                            DateOfBirth = x.DateOfBirth,
                            FullName = $"{x.FirstName} {x.MiddleName.Substring(0, 1)}. {x.LastName}",
                            Gender = x.Gender,
                            PatientId = x.PatientId,
                            PatientIdNo = x.PatientIdNo,
                            Rh = x.Rh
                        }
                    ).ToListAsync();

            var totalRecords = query.Count();

            if (results == null || !results.Any()) return pagedResult;

            pagedResult.TotalCount = totalRecords;
            pagedResult.Results = results.AsQueryable()
                .OrderBy(sortBy)
                .ToList();

            return pagedResult;
        }

        public async Task<PatientDto> GetPatientById(Guid id)
        {
            var patient = await repository.Patient.FindOneByConditionAsync(x => x.PatientId == id);
            if (patient == null)
            {
                throw new RecordNotFoundException("Patient not found.");
            }

            return mapper.Map<PatientDto>(patient);
        }
    }
}
