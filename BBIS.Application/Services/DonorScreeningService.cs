using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Common;
using BBIS.Common.Enums;
using BBIS.Common.Exceptions;
using BBIS.Common.Extensions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http.Json;

namespace BBIS.Application.Services
{
    public class DonorScreeningService : IDonorScreeningService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;

        public DonorScreeningService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper, HttpClient httpClient)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
            this.httpClient = httpClient;
        }

        public async Task<PagedSearchResultDto<DonorListDto>> GetDonors(DonorPagedSearchDto searchDto, List<string> roles)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "RegistrationDate desc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<DonorListDto>(searchDto);

            var screeningStatuses = dbContext.UserRoleScreeningAccess
            .Include(ura => ura.Role)
            .Where(ura => roles.Contains(ura.ScreeningTabName))
            .Select(ura => ura.ScreeningStatus)
            .ToList();

            //var statuses = roles.GetDonorStatusByRoles();

            //if (searchDto.DonorStatus == DonorStatus.Inventory)
            //{
            //    statuses.Add(DonorStatus.Inventory);
            //}


            var queryresults = dbContext.DonorTransactions
                .Include(x => x.Donor)
                .Include(x => x.DonorRegistration)
                .Include(x => x.DonorTestOrder)
                .Where(x => screeningStatuses.Contains(x.DonorStatus))
                .Search(t => t.Donor.Firstname, t => t.Donor.Lastname, t => t.Donor.Middlename)
                    .Containing(searchDto.Name)
                .Search(t => t.DonorRegistration.RegistrationNumber)
                    .Containing(searchDto.RegistrationNumber)
                //.Search(t => t.DonorInitialScreening.BloodType)
                //    .Containing(searchDto.BloodType)
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
                        //BloodType = string.IsNullOrEmpty(x.FinalBloodType) ? x.DonorInitialScreening.BloodType : x.FinalBloodType,
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
                string middlename = query.Donor.Middlename?.Substring(0, 1) ?? "";
                dto.DonorName = $"{query.Donor.Firstname} {middlename}. {query.Donor.Lastname}";
            }

            if (query.DonorTestOrder != null)
            {
                dto.HasTestOrder = true;
                dto.TestCompleted = query.DonorTestOrder.TestCompleted;
            }

            return dto;
        }
        public async Task<DonorVitalSignsDto> GetDonorVitalSignsInfo(Guid id)
        {
            var dto = new DonorVitalSignsDto();

            var query = await dbContext.DonorTransactions
                .Include(x => x.DonorVitalSigns)
                .Include(x => x.Donor)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);

            if (query == null)
                throw new RecordNotFoundException($"Donor transaction not found for registration ID: {id}");

            if (query.DonorVitalSigns != null)
            {
                dto = mapper.Map<DonorVitalSignsDto>(query.DonorVitalSigns);
                dto.DonorStatus = query?.DonorStatus;
            }
            dto.DonorStatus = query?.DonorStatus;
            dto.DonorTransactionId = query.DonorTransactionId;
            dto.DonorRegistrationId = id;
            dto.DonorName = $"{query.Donor.Firstname} {query.Donor.Middlename?.FirstOrDefault()}. {query.Donor.Lastname}";

            return dto;
        }

        public async Task<DonorCounselingDto> GetDonorCounselingInfo(Guid id)
        {
            var dto = new DonorCounselingDto();

            // Step 1: Load donor transaction by RegistrationId
            var donorTransaction = await dbContext.DonorTransactions
                .Include(x => x.Donor)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);

            if (donorTransaction == null)
                throw new RecordNotFoundException($"Donor transaction not found for registration ID: {id}");

            // Step 2: Get medical histories linked to the registration
            var histories = await dbContext.DonorMedicalHistories
                .Where(x => x.DonorRegistrationId == id)
                .AsNoTracking()
                .ToListAsync();

            dto.MedicalHistories = mapper.Map<List<DonorMedicalQuestionnaireDto>>(histories);

            // Step 3: Set remaining fields
            dto.DonorRegistrationId = donorTransaction.DonorRegistrationId;
            dto.DonorTransactionId = donorTransaction.DonorTransactionId;
            dto.DonorStatus = donorTransaction.DonorStatus;
            

            return dto;
        }

        public async Task<DonorBloodBagIssuanceDto> GetDonorBloodBagIssuance(Guid id)
        {

            var dto = new DonorBloodBagIssuanceDto();

            var query = await dbContext.DonorTransactions
                .Include(x => x.DonorBloodBagIssuances)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);

            if (query == null)
            {
                return dto;
            }
            try
            {
                var issuance = query.DonorBloodBagIssuances
                    .Where(x => !x.isFromModal)
                    .FirstOrDefault();
                var issuanceList = query.DonorBloodBagIssuances
                    .Where(x => !x.isFromModal)
                    .ToList();

                if (issuance != null)
                {
                    dto = mapper.Map<DonorBloodBagIssuanceDto>(issuance);
                    dto.DonorStatus = query?.DonorStatus;
                    dto.DonorRegistrationId = query.DonorRegistrationId;
                    dto.BloodBagInfos = mapper.Map<List<DonorBloodBagInfo>>(issuanceList);

                }
                else {
                    dto.DonorStatus = query?.DonorStatus;
                    dto.DonorRegistrationId = query.DonorRegistrationId;
                    dto.DonorTransactionId = query.DonorTransactionId;
                }


            }
            catch (AutoMapperMappingException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return dto;

            //try
            //{
            //    dto = mapper.Map<DonorBloodBagIssuanceDto>(query.DonorBloodBagIssuances);
            //}
            //catch (AutoMapperMappingException ex)
            //{
            //    Console.WriteLine(ex.ToString);
            //    throw;
            //}
            //return dto;
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
            else 
            {
                dto = mapper.Map<DonorPhysicalExaminationDto>(query.DonorPhysicalExamination);
                dto.DonorTransactionId = query.DonorTransactionId;
                dto.DonorRegistrationId = query.DonorRegistrationId;
                dto.DonorStatus = query.DonorStatus;
            }

           
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
                dto.SegmentSerialNumber = serialNumber;
                dto.DonorRegistrationId = id;
                dto.DonorTransactionId = query.DonorTransactionId;
                dto.DonorRegistrationId = query.DonorRegistrationId;
                dto.DonorStatus = query?.DonorStatus;
                dto.StartTime = DateTime.UtcNow;
                dto.EndTime = DateTime.UtcNow;
                return dto;
            }

            dto = mapper.Map<DonorBloodCollectionDto>(query.DonorBloodCollection);
            dto.DonorTransactionId = query.DonorTransactionId;
            dto.DonorRegistrationId = query.DonorRegistrationId;
            dto.DonorStatus = query.DonorStatus;
            dto.SegmentSerialNumber = serialNumber;
            //dto.PRCBloodDonorNumber = query.PRCBloodDonorNumber;
            dto.StartTime = DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc);
            dto.EndTime = DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc);
            return dto;
        }

        public async Task<DonorPostDonationCareDto> GetPostDonationCare(Guid id)
        {
            var dto = new DonorPostDonationCareDto();

            var query = await dbContext.DonorTransactions
                .Include(x => x.DonorPostDonationCare)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == id);

            if (query == null || query?.DonorPostDonationCare == null)
            {

                //dto = mapper.Map<DonorPostDonationCareDto>(query.DonorPostDonationCare);
                dto.DonorTransactionId = query.DonorTransactionId;
                dto.DonorStatus = query?.DonorStatus;
                return dto;
            }

          

            dto = mapper.Map<DonorPostDonationCareDto>(query.DonorPostDonationCare);
            dto.DonorTransactionId = query.DonorTransactionId;

            var postDonationDetail = await dbContext.PostDonationDetails
            .Where(x => x.DonorPostDonationCareId == dto.DonorPostDonationCareId)
            .ToListAsync();

            var vitalSignsMonitoring = await dbContext.VitalSignsMonitorings
            .Where(x => x.DonorPostDonationCareId == dto.DonorPostDonationCareId)
            .ToListAsync();

            dto.PostDonationListDetails = mapper.Map<List<PostDonationDetailsDto>>(postDonationDetail);

            dto.VitalSignsMonitoringDetails = mapper.Map<List<VitalSignsMonitoringDto>>(vitalSignsMonitoring);
            dto.DonorStatus = query?.DonorStatus;

            return dto;
        }

        public async Task<Guid?> GetScheduleIdFromTransaction(Guid id)
        {
            var scheduleId = await dbContext.DonorTransactions
                .Where(x => x.DonorRegistrationId == id)
                .Select(x => (Guid?)x.ScheduleId)
                .FirstOrDefaultAsync();

            return scheduleId;
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
        public async Task<Guid> UpdateDonorTransactionForBloodCollection(RegisteredDonorDto dto){

            var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(
                x => x.DonorRegistrationId == dto.DonorRegistrationId);

            if (donorTransaction == null)
                throw new RecordNotFoundException($"Donor transaction not found for registration ID: {dto.DonorRegistrationId}");


            donorTransaction.DonorStatus = DonorStatus.ForMethodBloodCollection;
            repository.DonorTransaction.Update(donorTransaction);

            await repository.SaveAsync();

            return donorTransaction.DonorRegistrationId;
        }

        public async Task<Guid> CreateUpdateDonorVitalSigns(DonorVitalSignsDto dto, Guid userId)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(
                x => x.DonorRegistrationId == dto.DonorRegistrationId);

            if (donorTransaction == null)
                throw new RecordNotFoundException($"Donor transaction not found for registration ID: {dto.DonorRegistrationId}");

            var entity = mapper.Map<DonorVitalSigns>(dto);
            entity.FacilitatedBy = userId;
            entity.DateOfExamination = DateTime.UtcNow;
            entity.DonorTransactionId = donorTransaction.DonorTransactionId;

            if (dto.DonorVitalSignsId.HasValue)
            {
                repository.DonorVitalSigns.Update(entity);
            }
            else
            {
                repository.DonorVitalSigns.Create(entity);
            }
            if (dto.DonorStatus == DonorStatus.Deferred)
            {
                await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
                donorTransaction.BloodIsSafeToTransfuse = false;
                donorTransaction.DateOfDonation = DateTime.UtcNow;
            }

            donorTransaction.DonorStatus = dto.DonorStatus;
            Console.WriteLine(entity.DonorVitalSignsId);
            repository.DonorTransaction.Update(donorTransaction);

            await repository.SaveAsync();
            return donorTransaction.DonorRegistrationId;
        }

        public async Task<Guid> CreateUpdateDonorBloodBagIssuance(DonorBloodBagIssuanceDto dto, Guid userId)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(
                x => x.DonorRegistrationId == dto.DonorRegistrationId);

            if (donorTransaction == null)
                throw new RecordNotFoundException($"Donor transaction not found for registration ID: {dto.DonorRegistrationId}");

            var donorBloodBagIssuance = mapper.Map<DonorBloodBagIssuance>(dto);

            var Issuance = mapper.Map<List<DonorBloodBagIssuance>>(dto.BloodBagInfos);

            foreach (var item in Issuance)
            {
                item.DonorTransactionId = donorTransaction.DonorTransactionId;
                item.IssuedBy = userId;
                donorTransaction.SegmentSerialNumber = item.SegmentSerialNumber;
                item.IssuedDate = DateTime.UtcNow;
            }

            if (dto.DonorBloodBagIssuanceId != Guid.Empty)
            {
                // Update each item individually
                foreach (var item in Issuance)
                {
                    item.DonorBloodBagIssuanceId = dto.DonorBloodBagIssuanceId ?? Guid.Empty;
                    repository.DonorBloodBagIssuance.Update(item);
                }
            }
            else
            {
                repository.DonorBloodBagIssuance.AddRange(Issuance);
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

        public async Task<Guid> CreateUpdateDonorCounseling(DonorCounselingDto dto, Guid regid)
        {
            try
            {
                
                if (dto == null) throw new ArgumentNullException(nameof(dto));

                var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorRegistrationId == dto.DonorRegistrationId);
                if (donorTransaction == null) throw new RecordNotFoundException($"Donor transaction does not exist for Id: {dto.DonorRegistrationId}");

                var counseling = mapper.Map<List<DonorMedicalHistory>>(dto.MedicalHistories);


                foreach (var item in counseling)
                {
                    item.DonorRegistrationId = donorTransaction.DonorRegistrationId;
                }

                if (dto.DonorMedicalHistoryID != Guid.Empty)
                {
                    // Update each item individually
                    foreach (var item in counseling)
                    {
                        repository.DonorMedicalHistory.Update(item);
                    }
                }
                else
                {
                    // Add all items at once
                    repository.DonorMedicalHistory.AddRange(counseling);
                }
                //if (dto.DonorStatus == DonorStatus.Deferred)
                //{
                //    await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
                //    donorTransaction.BloodIsSafeToTransfuse = false;
                //    donorTransaction.DateOfDonation = DateTime.UtcNow;
                //}

                donorTransaction.DonorStatus = dto.DonorStatus;
               
                repository.DonorTransaction.Update(donorTransaction);

                await repository.SaveAsync();

                return donorTransaction.DonorRegistrationId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateUpdateDonorCounseling: {ex.Message}");
                throw;
            }
        }

        public async Task<Guid> CreateUpdateDonorPostDonationCare(DonorPostDonationCareDto dto, Guid userid)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(dto));

                var donorTransaction = await repository.DonorTransaction
                    .FindOneByConditionAsync(x => x.DonorTransactionId == dto.DonorTransactionId);

                if (donorTransaction == null)
                    throw new RecordNotFoundException($"Donor transaction does not exist for Id: {dto.DonorTransactionId}");

                Guid pid = dto.DonorPostDonationCareId ?? Guid.NewGuid();
                bool isUpdate = dto.DonorPostDonationCareId.HasValue;

                // Map and set ID
                var donorCare = mapper.Map<DonorPostDonationCare>(dto);
                donorCare.DonorPostDonationCareId = pid;
                donorCare.MonitoredBy = userid;

                var vitalSignsMonitoringDetails = mapper.Map<List<VitalSignsMonitoring>>(dto.VitalSignsMonitoringDetails);
                var postDonationDetails = mapper.Map<List<PostDonationDetail>>(dto.PostDonationListDetails);

                foreach (var item in vitalSignsMonitoringDetails)
                {
                    item.DonorPostDonationCareId = pid;
                }

                foreach (var item in postDonationDetails)
                {
                    item.DonorPostDonationCareId = pid;
                }

                if (isUpdate)
                {
                    repository.DonorPostDonationCare.Update(donorCare);

                    // Optional: Remove old entries if you're not doing a full replace
                    // var existingVitalSigns = await repository.VitalSignsMonitoring
                    //     .FindByConditionAsync(x => x.DonorPostDonationCareId == pid);
                    // repository.VitalSignsMonitoring.RemoveRange(existingVitalSigns);

                    foreach (var item in vitalSignsMonitoringDetails)
                    {
                        repository.VitalSignsMonitoring.Update(item);
                    }
                    foreach (var item in postDonationDetails)
                    {
                        repository.PostDonationDetail.Update(item);
                    }
                }
                else
                {
                    repository.DonorPostDonationCare.Create(donorCare);
                    repository.VitalSignsMonitoring.AddRange(vitalSignsMonitoringDetails);
                    repository.PostDonationDetail.AddRange(postDonationDetails);
                }

                // Optional deferral logic
                // if (dto.DonorStatus == DonorStatus.Deferred)
                // {
                //     await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
                //     donorTransaction.BloodIsSafeToTransfuse = false;
                //     donorTransaction.DateOfDonation = DateTime.UtcNow;
                // }

                donorTransaction.DonorStatus = dto.DonorStatus;
                repository.DonorTransaction.Update(donorTransaction);

                await repository.SaveAsync();

                return donorTransaction.DonorRegistrationId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateUpdateDonorPostDonationCare: {ex.Message}");
                throw;
            }
        }

        //public async Task<Guid> CreateUpdateDonorPhysicalExamination(DonorPhysicalExaminationDto dto, Guid userId)
        //{
        //    try
        //    {
        //        if (dto == null) throw new ArgumentNullException(nameof(DonorPhysicalExaminationDto));

        //        // Check if DonorEventId exists
        //        var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorRegistrationId == dto.DonorRegistrationId);
        //        if (donorTransaction == null) throw new RecordNotFoundException($"Donor transaction does not exist for Id: {dto.DonorRegistrationId}");

        //        var physicalExamination = mapper.Map<DonorPhysicalExamination>(dto);
        //        physicalExamination.FacilitatedById = userId;
        //        physicalExamination.DateOfExamination = DateTime.UtcNow;
        //        physicalExamination.DonorTransactionId = donorTransaction.DonorTransactionId;

        //        if (dto.DonorPhysicalExaminationId.HasValue)
        //        {
        //            repository.DonorPhysicalExamination.Update(physicalExamination);
        //        }
        //        else
        //        {
        //            repository.DonorPhysicalExamination.Create(physicalExamination);
        //        }

        //        if (dto.DonorStatus == DonorStatus.Deferred)
        //        {
        //            await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
        //            donorTransaction.BloodIsSafeToTransfuse = false;
        //            donorTransaction.DateOfDonation = DateTime.UtcNow;
        //        }

        //        donorTransaction.DonorStatus = dto.DonorStatus;
        //        repository.DonorTransaction.Update(donorTransaction);

        //        await repository.SaveAsync();

        //        return donorTransaction.DonorRegistrationId;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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
                donorTransaction.BloodType = dto.BloodType?.Replace("+", "").Replace("-", "");
                donorTransaction.BloodRh = dto.BloodType.EndsWith("+") ? "+" :dto.BloodType.EndsWith("-") ? "-" : null;
                donorTransaction.DonorStatus = dto.DonorStatus;
                //donorTransaction.PRCBloodDonorNumber = dto.PRCBloodDonorNumber;
                repository.DonorTransaction.Update(donorTransaction);

                await repository.SaveAsync();

                return donorTransaction.DonorRegistrationId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<Guid> CreateUpdateDonorPostDonationCare(DonorPostDonationCareDto dto, Guid userId)
        //{
        //    try
        //    {
        //        if (dto == null) throw new ArgumentNullException(nameof(DonorPostDonationCareDto));

        //        // Check if DonorEventId exists
        //        var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorTransactionId == dto.DonorTransactionId);
        //        if (donorTransaction == null) throw new RecordNotFoundException($"Donor transaction does not exist for Id: {dto.DonorTransactionId}");

        //        var donorPostDonationCare = mapper.Map<DonorPostDonationCare>(dto);
        //        //donorPostDonationCare. = userId/*;*/
        //        //bloodCollection.DonorTransactionId = donorTransaction.DonorTransactionId;

        //        if (dto.DonorPostDonationCareId != Guid.Empty)
        //        {
        //            repository.DonorPostDonationCare.Update(donorPostDonationCare);
        //        }
        //        else
        //        {
        //            // Set Date of Donation after Blood Colection;
        //            //donorTransaction.DateOfDonation = DateTime.UtcNow;
        //            repository.DonorPostDonationCare.Create(donorPostDonationCare);
        //        }

        //        //if (dto.DonorStatus == DonorStatus.Deferred)
        //        //{
        //        //    await MarkDonorDeferred(donorTransaction.DonorTransactionId, dto.DeferralStatus, dto.Remarks);
        //        //    donorTransaction.BloodIsSafeToTransfuse = false;
        //        //    donorTransaction.DateOfDonation = DateTime.UtcNow;
        //        //}

        //        //donorTransaction.DonorStatus = dto.DonorStatus;
        //        ////donorTransaction.PRCBloodDonorNumber = dto.PRCBloodDonorNumber;
        //        repository.DonorTransaction.Update(donorTransaction);

        //        await repository.SaveAsync();

        //        return donorTransaction.DonorRegistrationId;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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
            var serialNumber = transaction.SegmentSerialNumber;

            if (string.IsNullOrEmpty(serialNumber))
            {
                // Generate a random byte array
                serialNumber = RandomStringGeneratorHelper.Generate("SN");
                transaction.SegmentSerialNumber = serialNumber;

                repository.DonorTransaction.Update(transaction);

                await repository.SaveAsync();
            }

            return serialNumber;
        }

        public async Task<string> CreateBBInventory(Guid TranasctionID)
        {
            // Use PostAsJsonAsync → simplest way

            //var bbinventoryDTO = new BBInventoryDTO

            var transaction = await dbContext.DonorTransactions
                .FirstOrDefaultAsync(x => x.DonorRegistrationId == TranasctionID);

            var bag = await dbContext.DonorBloodBagIssuances
            .FirstOrDefaultAsync(x => x.DonorTransactionId == transaction.DonorTransactionId);


            var screening = await dbContext.DonorInitialScreenings
                .FirstOrDefaultAsync(x => x.DonorTransactionId == transaction.DonorTransactionId);

            var collection = await dbContext.DonorBloodCollections
                .FirstOrDefaultAsync(x => x.DonorTransactionId == transaction.DonorTransactionId);

            if (transaction == null)
                throw new InvalidOperationException($"No transaction found for ID {TranasctionID}");

            if (bag == null)
                throw new InvalidOperationException($"No bag found for transaction {transaction.DonorTransactionId}");

            if (collection == null)
                throw new InvalidOperationException($"No collection found for transaction {transaction.DonorTransactionId}");

            // Screening can be optional, so you’re safe with null-conditional


            var dto = new BBInventoryDTO
            {
                bag_no = bag?.UnitSerialNumber ?? string.Empty,
                reg_no = bag?.SegmentSerialNumber ?? string.Empty,
                segmentserialno = bag?.SegmentSerialNumber ?? string.Empty,
                blood_group = transaction != null
                ? $"{transaction.BloodType} {(transaction.BloodRh == "+" ? "Positive" : "Negative")}"
                : string.Empty,
                bgroup = transaction?.BloodType ?? string.Empty,
                rh = transaction?.BloodRh == "+" ? "Positive" : "Negative",
                component = screening?.MethodOfBloodCollection ?? string.Empty,
                volume = collection?.CollectedBloodAmount.ToString() ?? "0",
                collection_date = collection?.EndTime ?? DateTime.UtcNow,
                expiry = (collection?.EndTime ?? DateTime.UtcNow).AddDays(35),
                has_allocation = collection != null && (
                !string.IsNullOrWhiteSpace(collection.PatientFirstName) ||
                !string.IsNullOrWhiteSpace(collection.PatientMiddleName) ||
                !string.IsNullOrWhiteSpace(collection.PatientLastName)),
                firstname_alloc = collection?.PatientFirstName,
                middlename_alloc = collection?.PatientMiddleName,
                lastname_alloc = collection?.PatientLastName,
                source = "VSMMC",
                bp_statusid = "017"
            };



            var response = await httpClient.PostAsJsonAsync("http://192.168.1.246:8080/api/bb-inventory", dto);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Laravel rejected the request: {responseContent}");
            }

            return responseContent;
        }

    }
}
