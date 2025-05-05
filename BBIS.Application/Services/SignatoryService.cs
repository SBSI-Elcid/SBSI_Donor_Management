using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Signatory;
using BBIS.Common.Exceptions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;

namespace BBIS.Application.Services
{
    public class SignatoryService : ISignatoryService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public SignatoryService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> UpsertSignatory(SignatoryDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(SignatoryDto));

                var signatory = mapper.Map<Signatory>(dto);

                if (dto.SignatoryId.HasValue)
                {
                    var existing = await repository.Signatory.FindOneByConditionAsync(x => x.SignatoryId == dto.SignatoryId);
                    if (existing == null) throw new RecordNotFoundException($"Signatory {dto.SignatoryId} does not exist.");

                    repository.Signatory.Update(signatory);
                }
                else
                {
                    repository.Signatory.Create(signatory);
                }

                await repository.SaveAsync();

                return signatory.SignatoryId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteSignatory(Guid id)
        {
            var signatory = await repository.Signatory.FindOneByConditionAsync(x => x.SignatoryId == id);
            if (signatory == null)
            {
                throw new RecordNotFoundException("Signatory not found.");
            }

            repository.Signatory.Delete(signatory);
            await repository.SaveAsync();
        }

        public async Task<PagedSearchResultDto<SignatoryViewDto>> GetPagedListAsync(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "FullName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<SignatoryViewDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.Signatories
                .Search(t => t.FirstName, t => t.MiddleName, t => t.LastName)
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
                        new SignatoryViewDto
                        {
                            SignatoryId = x.SignatoryId,
                            FullName = $"{x.FirstName} {x.MiddleName.Substring(0, 1)}. {x.LastName}",
                            Position = x.Position,
                            PositionPrefix = x.PositionPrefix,
                            LicenseNo = x.LicenseNo,
                            IsPathologist = x.IsPathologist,
                            IsActive = x.IsActive
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

        public async Task<SignatoryDto> GetSignatory(Guid id)
        {
            var signatory = await repository.Signatory.FindOneByConditionAsync(x => x.SignatoryId == id);
            if (signatory == null)
            {
                throw new RecordNotFoundException("Signatory not found.");
            }

            return mapper.Map<SignatoryDto>(signatory);
        }

        public async Task<List<SignatoryOptionDto>> GetSignatoryOptions()
        {
            var results = await dbContext.Signatories.Where(x => x.IsActive)
                .Select(x => 
                    new SignatoryOptionDto() { 
                        Name = $"{x.FirstName} {x.MiddleName.Substring(0,1)}. {x.LastName} {x.PositionPrefix}",
                        SignatoryId = x.SignatoryId,
                        IsPathologist = x.IsPathologist
                    }
                ).ToListAsync();

            return results;
        }
    }
}
