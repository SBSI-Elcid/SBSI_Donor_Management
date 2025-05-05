using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Common;
using BBIS.Common.Enums;
using BBIS.Common.Exceptions;
using BBIS.Common.Extensions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;

namespace BBIS.Application.Services
{
    public class DonorScreeningService : IDonorScreeningService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public DonorScreeningService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PagedSearchResultDto<DonorListDto>> GetDonors(DonorPagedSearchDto searchDto, List<string> roles)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "FullName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<DonorListDto>(searchDto);

            var statuses = roles.GetDonorStatusByRoles();

            if (searchDto.DonorStatus == DonorStatus.Inventory)
            {
                statuses.Add(DonorStatus.Inventory);
            }

            var queryresults = dbContext.DonorTransactions
                .Include(x => x.Donor)
                .Include(x => x.DonorRegistration)
                .Include(x => x.DonorTestOrder)
                .Where(x => statuses.Contains(x.DonorStatus))
                .Search(t => t.Donor.Firstname, t => t.Donor.Lastname, t => t.Donor.Middlename)
                    .Containing(searchDto.Name)
                .Search(t => t.DonorRegistration.RegistrationNumber)
                    .Containing(searchDto.RegistrationNumber)
                .Search(t => t.DonorInitialScreening.BloodType)
                    .Containing(searchDto.BloodType)
                .Search(t => t.DonorStatus)
                    .Containing(searchDto.DonorStatus);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = queryresults.Count();
            }

            var results = await queryresults
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x =>
                    new DonorListDto
                    {
                        DonorTransactionId = x.DonorTransactionId,
                        DonorRegistrationId = x.DonorRegistrationId,
                        RegistrationNumber = x.DonorRegistration.RegistrationNumber,
                        FullName = $"{x.Donor.Firstname} {x.Donor.Middlename.Substring(0, 1)}. {x.Donor.Lastname}",
                        CivilStatus = x.Donor.CivilStatus,
                        Gender = x.Donor.Gender,
                        BirthDate = x.Donor.BirthDate,
                        ContactNo = string.IsNullOrEmpty(x.Donor.MobileNo) ? x.Donor.TelNo : x.Donor.MobileNo,
                        BloodType = string.IsNullOrEmpty(x.FinalBloodType) ? x.DonorInitialScreening.BloodType : x.FinalBloodType,
                        DonorStatus = x.DonorStatus,
                        HasTestOrder = x.DonorTestOrder != null,
                        TestOrderCompleted = x.DonorTestOrder != null ? x.DonorTestOrder.TestCompleted : false,
                        RegistrationDate = x.DonorRegistration.RegistrationDate,
                        IsOffline = x.IsOffline,
                        IsSync = x.IsSync
                    }
                )
                .AsNoTracking()
                .ToListAsync();

            var totalRecords = queryresults.Count();

            if (results == null || !results.Any()) return pagedResult;

            pagedResult.TotalCount = totalRecords;
            pagedResult.Results = results.AsQueryable()
                .OrderBy(sortBy)
                .ToList();

            return pagedResult;
        }

        public async Task<DonorInitialScreeningDto> GetInitialScreeningInfo(Guid id)
        {
            var dto = new DonorInitialScreeningDto();

            var query = await dbContext.DonorTransactions
                .Include(x => x.DonorInitialScreening)
                .Include(x => x.Donor)
                    .ThenInclude(x => x.DonorRecentDonations)
                .Include(x => x.DonorTestOrder)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);
                        
            if (query == null || query?.DonorInitialScreening == null)
            {
                dto.DonorTransactionId = query.DonorTransactionId;
                dto.DonorRegistrationId = id;
                dto.DonorStatus = query?.DonorStatus;
            }
            else
            {
                dto = mapper.Map<DonorInitialScreeningDto>(query.DonorInitialScreening);
                dto.DonorTransactionId = query.DonorTransactionId;
                dto.DonorRegistrationId = query.DonorRegistrationId;
                dto.HasRecentDonations = query.Donor.DonorRecentDonations.Any();
                dto.DonorStatus = query.DonorStatus;
            }

            if (query.Donor != null)
            {
                dto.DonorName = $"{query.Donor.Firstname} {query.Donor.Middlename.Substring(0, 1)}. {query.Donor.Lastname}";
            }

            if (query.DonorTestOrder != null)
            {
                dto.HasTestOrder = true;
                dto.TestCompleted = query.DonorTestOrder.TestCompleted;
            }

            return dto;
        }

        public async Task<List<DonorRecentDonationDto>> GetRecentDonations(Guid id)
        {
            var dto = new List<DonorRecentDonationDto>();

            var query = await dbContext.DonorTransactions
                .Include(x => x.Donor)
                    .ThenInclude(x => x.DonorRecentDonations)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);

            if (query == null || query?.Donor.DonorRecentDonations == null)
            {
                var donation = new DonorRecentDonationDto();
                donation.DonorId = query.DonorId;

                dto.Add(donation);
                return dto;
            }

            dto = mapper.Map<List<DonorRecentDonationDto>>(query.Donor.DonorRecentDonations);
            return dto;
        }

        public async Task<DonorPhysicalExaminationDto> GetPhysicalExamInfo(Guid id)
        {
            var dto = new DonorPhysicalExaminationDto();

            var query = await dbContext.DonorTransactions
                .Include(x => x.DonorPhysicalExamination)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);

            if (query == null || query?.DonorPhysicalExamination == null)
            {
                dto.DonorRegistrationId = id;
                dto.DonorStatus = query?.DonorStatus;
                return dto;
            }

            dto = mapper.Map<DonorPhysicalExaminationDto>(query.DonorPhysicalExamination);
            dto.DonorTransactionId = query.DonorTransactionId;
            dto.DonorRegistrationId = query.DonorRegistrationId;
            dto.DonorStatus = query.DonorStatus;
            return dto;
        }

        public async Task<DonorBloodCollectionDto> GetBloodCollectionInfo(Guid id)
        {
            var dto = new DonorBloodCollectionDto();

            var query = await dbContext.DonorTransactions
                .Include(x => x.DonorBloodCollection)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);

            if (query == null)
            {
                throw new RecordNotFoundException("Donor transaction not found");
            }

            var serialNumber = await GetUnitSerialNumber(query);

            if (query?.DonorBloodCollection == null)
            {
                dto.UnitSerialNumber = serialNumber;
                dto.DonorRegistrationId = id;
                dto.DonorStatus = query?.DonorStatus;
                dto.StartTime = DateTime.UtcNow;
                dto.EndTime = DateTime.UtcNow;
                return dto;
            }

            dto = mapper.Map<DonorBloodCollectionDto>(query.DonorBloodCollection);
            dto.DonorTransactionId = query.DonorTransactionId;
            dto.DonorRegistrationId = query.DonorRegistrationId;
            dto.DonorStatus = query.DonorStatus;
            dto.UnitSerialNumber = serialNumber;
            dto.PRCBloodDonorNumber = query.PRCBloodDonorNumber;
            dto.StartTime = DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc);
            dto.EndTime = DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc);
            return dto;
        }

        public async Task<Guid> CreateUpdateDonorInitialScreening(DonorInitialScreeningDto dto, Guid userId)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(DonorInitialScreeningDto));

                // Check if DonorEventId exists
                var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorRegistrationId == dto.DonorRegistrationId);
                if (donorTransaction == null) throw new RecordNotFoundException($"Donor transaction does not exist for Id: {dto.DonorRegistrationId}");

                var initialScreening = mapper.Map<DonorInitialScreening>(dto);
                initialScreening.ScreenById = userId;
                initialScreening.ScreenDate = DateTime.UtcNow;
                initialScreening.DonorTransactionId = donorTransaction.DonorTransactionId;

                if(dto.DonorInitialScreeningId.HasValue)
                {
                   repository.DonorInitialScreening.Update(initialScreening);
                }
                else
                {
                   repository.DonorInitialScreening.Create(initialScreening);
                }

                if (dto.RecentDonations != null)
                {
                    var recentDonation = mapper.Map<List<DonorRecentDonation>>(dto.RecentDonations);
                    foreach (var item in recentDonation)
                    {
                        item.DonorId = donorTransaction.DonorId;
                    }

                    repository.DonorRecentDonation.AddRange(recentDonation);
                }

                if (dto.DonorStatus == DonorStatus.Deferred)
                {
                    await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
                    donorTransaction.BloodIsSafeToTransfuse = false;
                    donorTransaction.DateOfDonation = DateTime.UtcNow;
                }

                donorTransaction.DonorStatus = dto.DonorStatus;
                repository.DonorTransaction.Update(donorTransaction);

                await repository.SaveAsync();

                return donorTransaction.DonorRegistrationId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> CreateUpdateDonorPhysicalExamination(DonorPhysicalExaminationDto dto, Guid userId)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(DonorPhysicalExaminationDto));

                // Check if DonorEventId exists
                var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorRegistrationId == dto.DonorRegistrationId);
                if (donorTransaction == null) throw new RecordNotFoundException($"Donor transaction does not exist for Id: {dto.DonorRegistrationId}");

                var physicalExamination = mapper.Map<DonorPhysicalExamination>(dto);
                physicalExamination.FacilitatedById = userId;
                physicalExamination.DateOfExamination = DateTime.UtcNow;
                physicalExamination.DonorTransactionId = donorTransaction.DonorTransactionId;

                if (dto.DonorPhysicalExaminationId.HasValue)
                {
                    repository.DonorPhysicalExamination.Update(physicalExamination);
                }
                else
                {
                    repository.DonorPhysicalExamination.Create(physicalExamination);
                }

                if (dto.DonorStatus == DonorStatus.Deferred)
                {
                    await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
                    donorTransaction.BloodIsSafeToTransfuse = false;
                    donorTransaction.DateOfDonation = DateTime.UtcNow;
                }

                donorTransaction.DonorStatus = dto.DonorStatus;
                repository.DonorTransaction.Update(donorTransaction);

                await repository.SaveAsync();

                return donorTransaction.DonorRegistrationId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> CreateUpdateDonorBloodCollection(DonorBloodCollectionDto dto, Guid userId)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(DonorBloodCollectionDto));

                // Check if DonorEventId exists
                var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorRegistrationId == dto.DonorRegistrationId);
                if (donorTransaction == null) throw new RecordNotFoundException($"Donor transaction does not exist for Id: {dto.DonorRegistrationId}");

                var bloodCollection = mapper.Map<DonorBloodCollection>(dto);
                bloodCollection.FacilitatedById = userId;
                bloodCollection.StartTime = dto.StartTime;
                bloodCollection.EndTime = dto.EndTime;
                bloodCollection.DonorTransactionId = donorTransaction.DonorTransactionId;

                if (dto.DonorBloodCollectionId.HasValue)
                {
                    repository.DonorBloodCollection.Update(bloodCollection);
                }
                else
                {
                    // Set Date of Donation after Blood Colection;
                    donorTransaction.DateOfDonation = DateTime.UtcNow;
                    repository.DonorBloodCollection.Create(bloodCollection);
                }

                if (dto.DonorStatus == DonorStatus.Deferred)
                {
                    await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
                    donorTransaction.BloodIsSafeToTransfuse = false;
                    donorTransaction.DateOfDonation = DateTime.UtcNow;
                }

                donorTransaction.DonorStatus = dto.DonorStatus;
                donorTransaction.PRCBloodDonorNumber = dto.PRCBloodDonorNumber;
                repository.DonorTransaction.Update(donorTransaction);

                await repository.SaveAsync();

                return donorTransaction.DonorRegistrationId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task MarkDonorDeferred(Guid transactionId,  string deferralStatus, string remarks)
        {
            try
            {
                var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorTransactionId == transactionId);
                if (donorTransaction == null) throw new RecordNotFoundException($"Donor transaction does not exist for Id: {transactionId}");

                var deferralDto = new DonorDeferralDto
                {
                    DonorTransactionId = transactionId,
                    DeferralStatus = deferralStatus,
                    Remarks = remarks
                };

                var donorDeferral = mapper.Map<DonorDeferral>(deferralDto);
                repository.DonorDeferral.Create(donorDeferral);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetUnitSerialNumber(DonorTransaction transaction)
        {
            var serialNumber = transaction.UnitSerialNumber;

            if (string.IsNullOrEmpty(serialNumber))
            {
                // Generate a random byte array
                serialNumber = RandomStringGeneratorHelper.Generate("SN");
                transaction.UnitSerialNumber = serialNumber;

                repository.DonorTransaction.Update(transaction);

                await repository.SaveAsync();
            }

            return serialNumber;
        }
    }
}
