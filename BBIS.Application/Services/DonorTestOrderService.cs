using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Application.DTOs.DonorTestOrder;
using BBIS.Common.Enums;
using BBIS.Common.Exceptions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using NinjaNye.SearchExtensions;
using System.Data;
using BBIS.Common.Extensions;

namespace BBIS.Application.Services
{
    public class DonorTestOrderService : IDonorTestOrderService
    {
        private readonly BBDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;

        public DonorTestOrderService(BBDbContext dbContext, IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.repository = repositoryWrapper;
        }

        public async Task<List<BloodTestTypeDto>> GetBloodTestTypes()
        {
            var testTypes = await dbContext.BloodTestTypes
                 .Where(x => x.IsActive)
                 .AsNoTracking()
                 .Select(x => new BloodTestTypeDto { BloodTestTypeId = x.BloodTestTypeId, TypeName = x.TypeName })
                 .ToListAsync();

            return testTypes;
        }

        public async Task<PagedSearchResultDto<DonorTestOrderListDto>> GetDonorTestOrders(DonorTestOrderPagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(DonorTestOrderPagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "FullName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<DonorTestOrderListDto>(searchDto);

            searchDto.DateFrom = searchDto.DateFrom.ConvertFilterToUtcStartOfDay();
            searchDto.DateTo = searchDto.DateTo.ConvertFilterToUtcEndOfDay();

            var query = dbContext.DonorTestOrders
                .Include(x => x.DonorTestOrderTestTypes)
                .Include(x => x.DonorTransaction)
                    .ThenInclude(x => x.Donor)
                .Where(x => (searchDto.DateFrom == null || searchDto.DateTo == null || x.DateCreated >= searchDto.DateFrom.Value && x.DateCreated <= searchDto.DateTo.Value))
                .Search(t => t.DonorTransaction.DonorRegistration.RegistrationNumber, t => t.DonorTransaction.Donor.Firstname, t => t.DonorTransaction.Donor.Lastname, t => t.DonorTransaction.Donor.Middlename)
                    .Containing(searchDto.SearchText);

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count();
            }

            var results = await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x =>
                    new DonorTestOrderListDto
                    {
                        DonorTestOrderId = x.DonorTestOrderId,
                        DonorTransactionId = x.DonorTransactionId,
                        RegistrationNumber = x.DonorTransaction.DonorRegistration.RegistrationNumber,
                        FullName = $"{x.DonorTransaction.Donor.Firstname} {x.DonorTransaction.Donor.Middlename.Substring(0, 1)}. {x.DonorTransaction.Donor.Lastname}",
                        FinalBloodType = x.DonorTransaction.BloodType,
                        TestTypes = x.DonorTestOrderTestTypes
                            .Select(x => new DonorTestOrderTypesDto
                            {
                                BloodTestTypeId = x.BloodTestTypeId,
                                DonorTestOrderTestTypeId = x.DonorTestOrderTestTypeId,
                                IsReactive = x.IsReactive,
                                Remarks = x.Remarks,
                                TestTypeName = x.BloodTestType.TypeName
                            }).ToList(),
                        IsReactive = x.DonorTestOrderTestTypes.Any(x => x.IsReactive == true),
                        TestCompleted = x.TestCompleted,
                        DateCreated = x.DateCreated,
                    }
                )
                .AsNoTracking()
                .ToListAsync();

            var totalRecords = query.Count();

            if (results == null || !results.Any()) return pagedResult;

            pagedResult.TotalCount = totalRecords;
            pagedResult.Results = results.AsQueryable()
                .OrderBy(sortBy)
                .ToList();

            return pagedResult;
        }

        public async Task<DonorTestOrderDto> GetDonorTestOrder(Guid transactionId)
        {
            var query = await dbContext.DonorTransactions
                .Include(x => x.DonorInitialScreening)
                .Include(x => x.DonorTestOrder)
                    .ThenInclude(x => x.DonorTestOrderTestTypes)
                        .ThenInclude(x => x.BloodTestType)
                .FirstOrDefaultAsync(x => x.DonorTransactionId == transactionId);

            if (query == null)
            {
                return null;
            }

            var testOrder = query.DonorTestOrder;

            var dto = new DonorTestOrderDto
            {
                DonorTestOrderId = testOrder.DonorTestOrderId,
                DonorTransactionId = transactionId,
                TestCompleted = testOrder.TestCompleted,
                //FinalBloodType = string.IsNullOrEmpty(query.FinalBloodType) ? query.DonorInitialScreening?.BloodType : query.FinalBloodType,
                BloodRh = query.BloodRh,
                TestTypes = testOrder.DonorTestOrderTestTypes
                    .Select(x => new DonorTestOrderTypesDto
                    {
                        BloodTestTypeId = x.BloodTestTypeId,
                        DonorTestOrderTestTypeId = x.DonorTestOrderTestTypeId,
                        IsReactive = x.IsReactive,
                        Remarks = x.Remarks,
                        TestTypeName = x.BloodTestType.TypeName
                    }).ToList()
            };

            return dto;
        }

        public async Task<Guid> CreateDonorTestOrder(CreateDonorTestOrderDto dto, Guid userId)
        {
            var donorTransaction = await dbContext.DonorTransactions
                .Include(x => x.DonorTestOrder)
                .FirstOrDefaultAsync(x => x.DonorTransactionId == dto.DonorTransactionId);

            if(donorTransaction == null)
            {
                throw new RecordNotFoundException($"Donor transaction not found for Id: {dto.DonorTransactionId}");
            }

            if(donorTransaction.DonorTestOrder != null)
            {
                return donorTransaction.DonorTestOrder.DonorTestOrderId;
            }

            var newTestOrder = new DonorTestOrder()
            {
                CreatedById = userId,
                DateCreated = DateTime.UtcNow,
                DonorTransactionId = dto.DonorTransactionId,
                TestCompleted = false
            };

            if(dto.TestTypes.Any())
            {
                newTestOrder.DonorTestOrderTestTypes = new List<DonorTestOrderTestType>();

                dto.TestTypes.ForEach(item => 
                {
                    var testType = new DonorTestOrderTestType
                    {
                        BloodTestTypeId = item,
                        DonorTestOrderId = newTestOrder.DonorTestOrderId,
                        IsReactive = false,
                        Remarks = "",
                        UpdatedById = userId,
                    };

                    newTestOrder.DonorTestOrderTestTypes.Add(testType);
                });
            }

            await dbContext.DonorTestOrders.AddAsync(newTestOrder);

            await dbContext.SaveChangesAsync();

            return newTestOrder.DonorTestOrderId;
        }

        public async Task<bool> UpdateDonorTestOrder(DonorTestOrderDto dto, Guid userId)
        {
            var donorTransaction = await dbContext.DonorTransactions
               .Include(x => x.DonorInitialScreening)
               .Include(x => x.Donor)
               .Include(x => x.DonorTestOrder)
               .FirstOrDefaultAsync(x => x.DonorTransactionId == dto.DonorTransactionId);

            if (donorTransaction == null)
            {
                throw new RecordNotFoundException($"Donor transaction not found for Id: {dto.DonorTransactionId}");
            }

            var testOrder = await dbContext.DonorTestOrders
                .Include(x => x.DonorTestOrderTestTypes)
                .FirstOrDefaultAsync(x => x.DonorTestOrderId == donorTransaction.DonorTestOrder.DonorTestOrderId);

            if (testOrder == null)
            {
                throw new RecordNotFoundException($"Donor test order not found for Id: {dto.DonorTestOrderId}");
            }

            testOrder.TestCompleted = dto.TestCompleted;

            foreach(var item in dto.TestTypes)
            {
                var typeOfTest = testOrder.DonorTestOrderTestTypes.FirstOrDefault(x => x.DonorTestOrderTestTypeId == item.DonorTestOrderTestTypeId);
                if(typeOfTest != null)
                {
                    typeOfTest.IsReactive = item.IsReactive;
                    typeOfTest.Remarks = item.Remarks;
                    typeOfTest.DateUpdated = DateTime.UtcNow;
                    typeOfTest.UpdatedById = userId;
                }
            }

            if (!string.IsNullOrEmpty(dto.FinalBloodType))
            {
                donorTransaction.BloodType = dto.FinalBloodType;
                //donorTransaction.DonorInitialScreening.BloodType = dto.FinalBloodType;
            }

            if (!string.IsNullOrEmpty(dto.BloodRh))
            {
                donorTransaction.BloodRh = dto.BloodRh;
            }

            var testSucceeded = true;

            // Mark as deffered if one of tests is reactive
            if (dto.TestTypes.Any(x => x.IsReactive) && dto.TestCompleted)
            {
                testSucceeded = false;

                var testTypeNames = dto.TestTypes.Where(x => x.IsReactive)
                    .Select(x => x.TestTypeName);
                var reasons = $"{string.Join(", ", testTypeNames)} was Reactive";

                donorTransaction.DonorStatus = DonorStatus.Deferred;
              
                await MarkDonorDeferred(dto.DonorTransactionId, DeferralStatus.Permanent, reasons);
                await repository.SaveAsync();
            }
            else
            {
                donorTransaction.DonorStatus = DonorStatus.Success;
                testSucceeded = true;
            }

            await dbContext.SaveChangesAsync();

            return testSucceeded;
        }

        private async Task MarkDonorDeferred(Guid transactionId, string deferralStatus, string remarks)
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
    }
}
