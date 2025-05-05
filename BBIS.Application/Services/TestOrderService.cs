using AutoMapper;
using BBIS.Application.ConnectionProvider;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.TestOrder;
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
    public class TestOrderService : ITestOrderService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly IConnectionProvider dbConnection;

        public TestOrderService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper, IConnectionProvider connection)
        {
            this.dbConnection = connection;
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateTestOrder(CreateTestOrderDto dto, Guid userId)
        {
            var testOrder = new TestOrder()
            {
                TestOrderNumber = RandomStringGeneratorHelper.Generate("TO"),
                PatientId = dto.PatientId,
                ReservationId = dto.ReservationId,
                TestCompleted = false,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = userId,
                UpdatedDate = DateTime.UtcNow,
                Remarks  = "",
            };

            foreach (var testOrderTypeId in dto.TestOrders)
            {
                if (testOrderTypeId == TestOrderTypeEnum.BloodScreening)
                {
                    testOrder.BloodScreeningTestOrder = new BloodScreeningTestOrder();
                }

                if (testOrderTypeId == TestOrderTypeEnum.CoombsTest)
                {
                    testOrder.CoombsTestOrder = new CoombsTestOrder();
                }

                if (testOrderTypeId == TestOrderTypeEnum.BloodTyping)
                {
                    testOrder.BloodTypingTestOrder = new BloodTypingTestOrder();
                }
            }

            if (dto.CrossMatchTestOrders != null)
            {
                testOrder.CrossMatchTestOrders = new List<CrossMatchTestOrder>();

                foreach (var item in dto.CrossMatchTestOrders)
                {
                    var xmatch = new CrossMatchTestOrder()
                    {
                        BloodComponentId= item.BloodComponentId,
                        CollectionDate= item.CollectionDate,
                        DonorTransactionId=item.DonorTransactionId,
                        ExpiryDate= item.ExpiryDate,
                        Source = item.Source,
                        CrossMatchType = item.CrossMatchType,
                        DonorUnitSerialNumber = item.DonorUnitSerialNumber,
                        Result = item.Result
                    };

                    testOrder.CrossMatchTestOrders.Add(xmatch);
                }
            }

            repository.TestOrderGroup.Create(testOrder);

            // If a test order is created and the status was previously "Received", update it to "In Progress".
            var reservation = await repository.Reservation.FindOneByConditionAsync(x => x.ReservationId == dto.ReservationId);
            if (reservation.Status == RequisitionStatus.Received)
            {
                reservation.Status = RequisitionStatus.InProgress;
                repository.Reservation.Update(reservation);
            }

            await repository.SaveAsync();

            return testOrder.TestOrderId;
        }

        public async Task DeleteTestOrder(Guid id)
        {
            var query = await dbContext.TestOrders.FirstOrDefaultAsync(x => x.TestOrderId == id);

            if (query == null)
            {
                throw new RecordNotFoundException($"Record not found for TestOrderGroup with Id: {id}");
            }

            if(!query.TestCompleted)
            {
               // dbContext.TestOrders.RemoveRange(query.TestOrders);
                dbContext.TestOrders.Remove(query);

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<TestOrderDto> GetTestOrder(Guid testOrderId)
        {
            var query = await dbContext.TestOrders
                .Include(x => x.Patient)
                .Include(x => x.CrossMatchTestOrders).ThenInclude(x => x.BloodComponent)
                .Include(x => x.BloodScreeningTestOrder)
                .Include(x => x.BloodTypingTestOrder)
                .Include(x => x.CoombsTestOrder)
                .FirstOrDefaultAsync(x => x.TestOrderId == testOrderId);

            if (query == null)
            {
                throw new RecordNotFoundException($"Record not found for TestOrderGroup with Id: {testOrderId}");
            }

            var result = new TestOrderDto
            {
                TestOrderId = query.TestOrderId,
                TestOrderNumber = query.TestOrderNumber,
                PatientId = query.PatientId,
                PatientName = query.Patient != null ? $"{query.Patient.FirstName} {query.Patient.MiddleName.Substring(0, 1)}. {query.Patient.LastName}" : "",
                ReservationId = query.ReservationId,
                TestCompleted = query.TestCompleted,
                Remarks = query.Remarks, 
                PerformedBy = query.PerformedBy,
                ValidatedBy = query.ValidatedBy,
                ReviewedBy = query.ReviewedBy,
                PathologistId = query.PhatologistId,

                GroupCrossMatchTestOrders = new List<GroupCrossMatchTestOrderViewDto>()
            };

            if (query.CrossMatchTestOrders.Any())
            {
                var bloodComponents = query.CrossMatchTestOrders
                    .GroupBy(x => x.BloodComponentId).Select(x => x.First())
                    .Select(s => s.BloodComponentId).ToList();

                var results = new List<GroupCrossMatchTestOrderViewDto>();

                foreach (var componentId in bloodComponents)
                {
                    var items = query.CrossMatchTestOrders.Where(x => x.BloodComponentId == componentId);

                    var group = new GroupCrossMatchTestOrderViewDto()
                    {
                        BloodComponentId = componentId,
                        BloodComponentName = items.FirstOrDefault().BloodComponent.ComponentName,
                        CrossMatchTestOrders = items.Select(x => new CrossMatchTestOrderDto()
                        {
                            BloodComponentId = x.BloodComponentId,
                            BloodComponentName = x.BloodComponent.ComponentName,
                            CrossMatchType = x.CrossMatchType,
                            DonorTransactionId = x.DonorTransactionId,
                            DonorUnitSerialNumber = x.DonorUnitSerialNumber,
                            Result = x.Result,
                            Source = x.Source,
                            LISS_AGH = x.LISS_AGH,
                            CrossMatchTestOrderId= x.CrossMatchTestOrderId,
                            ExpiryDate = x.ExpiryDate
                        }).ToList()
                    };

                    results.Add(group);
                }

                result.GroupCrossMatchTestOrders = results;
            }

            if (query.BloodTypingTestOrder != null)
            {
                var order = query.BloodTypingTestOrder;
                result.BloodTypingTestOrder = new BloodTypingTestOrderDto() { 
                    BloodRh = order.BloodRh,
                    BloodType = order.BloodType,
                    BloodTypingTestOrderId = order.BloodTypingTestOrderId,
                    Control = order.Control,
                    FTAntiA = order.FTAntiA,
                    FTAntiAB = order.FTAntiAB,
                    FTAntiB = order.FTAntiB,
                    FTAntiD = order.FTAntiD,
                    FTAntiD2 = order.FTAntiD2,
                    RTACells = order.RTACells,
                    RTBCells = order.RTBCells
                };
            }

            if (query.BloodScreeningTestOrder != null)
            {
                var order = query.BloodScreeningTestOrder;
                result.BloodScreeningTestOrder = new BloodScreeningTestOrderDto()
                {
                    Result = order.Result,
                    BloodScreeningTestOrderId = order.BloodScreeningTestOrderId
                };
            }

            if (query.CoombsTestOrder != null)
            {
                var order = query.CoombsTestOrder;
                result.CoombsTestOrder = new CoombsTestOrderDto()
                {
                    CoombsTestOrderId = order.CoombsTestOrderId,
                    DATResult = order.DATResult,
                    IATResult = order.IATResult
                };
            }

            return result;
        }

        public async Task<List<TestOrderTypeDto>> GetTestOrderTypes()
        {
            var result = await dbContext.TestOrderTypes.Where(x => x.IsActive)
                .Select(x => new TestOrderTypeDto { Name = x.Name, TestOrderTypeId = x.TestOrderTypeId } )
                .ToListAsync();

            return result.OrderByDescending(x => x.Name).ToList();
        }

        public async Task<List<TypeAheadResultDto>> SearchDonors(string searchText, string bt)
        {
            var results = await dbContext.DonorTransactions
                .Include(x => x.Donor)
                .Where(x => x.DonorStatus == DonorStatus.Inventory && x.FinalBloodType == bt)
                .Search(x => x.Donor.Firstname, x => x.Donor.Middlename, x => x.Donor.Lastname)
                .Containing(searchText)
                .Select(x =>
                    new TypeAheadResultDto
                    {
                        Id = x.DonorId,
                        Name = $"{x.Donor.Firstname} {x.Donor.Middlename.Substring(0, 1)}. {x.Donor.Lastname}",
                        BloodType = x.FinalBloodType
                    }).ToListAsync();

            if (results == null)
            {
                return new List<TypeAheadResultDto>();
            }

            return results;
        }

        public async Task<List<TypeAheadResultDto>> SearchPatients(string searchText)
        {
            var results = new List<TypeAheadResultDto>();

            results = await dbContext.Patients
                .Search(x => x.FirstName, x => x.MiddleName, x => x.LastName)
                .Containing(searchText)
                .Select(x =>
                    new TypeAheadResultDto
                    {
                        Id = x.PatientId,
                        Name = $"{x.FirstName} {x.MiddleName.Substring(0, 1)}. {x.LastName}",
                        BloodType = x.BloodType
                    }).ToListAsync();
                      
            return results;
        }

        public async Task<bool> UpdateTestOrder(UpdateTestOrderDto dto, Guid userId)
        {
            var testOrder = await dbContext.TestOrders
                .Include(x => x.CrossMatchTestOrders)
                .Include(x => x.BloodScreeningTestOrder)
                .Include(x => x.BloodTypingTestOrder)
                .Include(x => x.CoombsTestOrder)
                .Include(x => x.Reservation)
                    .ThenInclude(x => x.ReservationItems)
                .FirstOrDefaultAsync(x => x.TestOrderId == dto.TestOrderId);

            if (testOrder == null)
            {
                throw new RecordNotFoundException("Record not found for TestOrder");
            }
            
            if (dto.BloodTypingTestOrder != null)
            {
                testOrder.BloodTypingTestOrder.BloodRh = dto.BloodTypingTestOrder.BloodRh;
                testOrder.BloodTypingTestOrder.Control = dto.BloodTypingTestOrder.Control;
                testOrder.BloodTypingTestOrder.BloodType = dto.BloodTypingTestOrder.BloodType;
                testOrder.BloodTypingTestOrder.FTAntiA = dto.BloodTypingTestOrder.FTAntiA;
                testOrder.BloodTypingTestOrder.FTAntiB = dto.BloodTypingTestOrder.FTAntiB;
                testOrder.BloodTypingTestOrder.FTAntiAB = dto.BloodTypingTestOrder.FTAntiAB;
                testOrder.BloodTypingTestOrder.FTAntiD = dto.BloodTypingTestOrder.FTAntiD;
                testOrder.BloodTypingTestOrder.FTAntiD2 = dto.BloodTypingTestOrder.FTAntiD2;
                testOrder.BloodTypingTestOrder.RTACells = dto.BloodTypingTestOrder.RTACells;
                testOrder.BloodTypingTestOrder.RTBCells = dto.BloodTypingTestOrder.RTBCells;
                testOrder.BloodTypingTestOrder.DateUpdated = DateTime.UtcNow;
            }

            if (dto.BloodScreeningTestOrder != null)
            {
                testOrder.BloodScreeningTestOrder.Result = dto.BloodScreeningTestOrder.Result;
                testOrder.BloodScreeningTestOrder.DateUpdated = DateTime.UtcNow;
            }

            if (dto.CoombsTestOrder != null)
            {
                testOrder.CoombsTestOrder.DATResult = dto.CoombsTestOrder.DATResult;
                testOrder.CoombsTestOrder.IATResult = dto.CoombsTestOrder.IATResult;
                testOrder.CoombsTestOrder.DateUpdated = DateTime.UtcNow;
            }

            if (dto.CrossMatchTestOrders.Any())
            {
                foreach (var item in dto.CrossMatchTestOrders)
                {
                    var xmatch = testOrder.CrossMatchTestOrders.FirstOrDefault(x => x.CrossMatchTestOrderId == item.CrossMathTestOrderId);
                    if (xmatch != null)
                    {
                        xmatch.LISS_AGH = item.LISS_AGH;
                        xmatch.Result = item.Result;
                        xmatch.CrossMatchType = item.CrossMatchType;

                        // If cross matching is not compatible and test is completed, return the inventory item to InStock status 
                        if (xmatch.Result == CompatibilityOptions.Incompatible && dto.TestCompleted)
                        {
                            var reservationItem = testOrder.Reservation.ReservationItems.FirstOrDefault(x => x.BloodComponentId == xmatch.BloodComponentId && x.DonorTransactionId == xmatch.DonorTransactionId);
                           
                            if (reservationItem != null)
                            {
                                var inventoryItem = await dbContext.InventoryItems.FirstOrDefaultAsync(x => x.InventoryItemId == reservationItem.InventoryItemId);
                                if (inventoryItem != null && inventoryItem.Status == InventoryStatus.Reserved)
                                {
                                    inventoryItem.Status = InventoryStatus.InStock;
                                }
                            }
                        }
                    }
                }
            }

            testOrder.PhatologistId = dto.PathologistId;
            testOrder.PerformedBy = dto.PerformedBy;
            testOrder.ReviewedBy = dto.ReviewedBy;
            testOrder.ValidatedBy= dto.ValidatedBy;
            testOrder.TestCompleted = dto.TestCompleted;
            testOrder.Remarks = dto.Remarks;
            testOrder.UpdatedBy = userId;
            testOrder.UpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<PagedSearchResultDto<RequestedTestOrderListDto>> SearchTestOrders(RequestedTestOrderPagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(RequestedTestOrderPagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "PatientName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<RequestedTestOrderListDto>(searchDto);

            searchDto.DateFrom = searchDto.DateFrom.ConvertFilterToUtcStartOfDay();
            searchDto.DateTo = searchDto.DateTo.ConvertFilterToUtcEndOfDay();

            var query = await dbContext.TestOrders
                .Include(x => x.Patient)
                .Include(x => x.Reservation)
                .Include(x => x.CrossMatchTestOrders)
                .Include(x => x.BloodScreeningTestOrder)
                .Include(x => x.BloodTypingTestOrder)
                .Include(x => x.CoombsTestOrder)
                .Where(x => x.Reservation.Status != RequisitionStatus.Cancelled &&
                        (searchDto.DateFrom == null || searchDto.DateTo == null || x.CreatedDate.Value >= searchDto.DateFrom.Value && x.CreatedDate.Value <= searchDto.DateTo.Value))
                .Search(t => t.Patient.FirstName, t => t.Patient.LastName, t => t.Patient.MiddleName).Containing(searchDto.SearchText)
                .AsNoTracking()
                .ToListAsync();

            // If the items per page selected is "All" the underlying value of it is -1, so use the total count of data as page size.
            if (searchDto.PageSize == -1)
            {
                searchDto.PageSize = query.Count;
            }

            var results = query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .Select(x => new RequestedTestOrderListDto()
                {
                    TestOrderNumber = x.TestOrderNumber,
                    ReservationId = x.ReservationId,
                    TestOrderId = x.TestOrderId,
                    PatientName = $"{x.Patient.FirstName} {x.Patient.MiddleName.Substring(0, 1)}. {x.Patient.LastName}",
                    TestCompleted = x.TestCompleted,
                    TestOrdersResult = GetTestOrders(x),
                    CrossMatchTestOrdersResult = x.CrossMatchTestOrders
                        .Select(x =>
                            new CrossMatchTestOrderViewDto {
                                CrossMatchTestOrderId = x.CrossMatchTestOrderId,
                                CrossMatchType = x.CrossMatchType,
                                DonorUnitSerialNumber = x.DonorUnitSerialNumber,
                                Result = x.Result
                            }
                         ).ToList(),
                    DateCreated = x.CreatedDate.GetValueOrDefault()
                })
                .ToList();

            var totalRecords = query.Count;

            if (results == null || !results.Any()) return pagedResult;

            pagedResult.TotalCount = totalRecords;
            pagedResult.Results = results.AsQueryable()
                .OrderBy(sortBy)
                .ToList();

            return pagedResult;
        }  
        
        private List<RequestedTestOrderDto> GetTestOrders(TestOrder order)
        {
            var results = new List<RequestedTestOrderDto>();

            if (order.CrossMatchTestOrders.Any())
            {
                results.Add(new RequestedTestOrderDto()
                {
                    Result = $"",
                    TestOrderName = $"Compatibility Test - ({order.CrossMatchTestOrders.Count})",
                });
            }

            if (order.CoombsTestOrder != null)
            {
                var datResult = string.IsNullOrEmpty(order.CoombsTestOrder.DATResult) ? "No results available." : order.CoombsTestOrder.DATResult;
                var iatResult = string.IsNullOrEmpty(order.CoombsTestOrder.IATResult) ? "No results available." : order.CoombsTestOrder.IATResult;

                results.Add(new RequestedTestOrderDto()
                {
                    Result = $"DAT: {datResult}     IAT: {iatResult}",
                    TestOrderName = "Coombs Test",
                    TestOrderTypeId = TestOrderTypeEnum.CoombsTest
                });
            }

            if (order.BloodTypingTestOrder != null)
            {
                var bloodType = string.IsNullOrEmpty(order.BloodTypingTestOrder.BloodType)  ? "No results available." : order.BloodTypingTestOrder.BloodType;
                var bloodRh = string.IsNullOrEmpty(order.BloodTypingTestOrder.BloodRh) ? "No results available." : order.BloodTypingTestOrder.BloodRh;

                results.Add(new RequestedTestOrderDto()
                {
                    Result = $"Blood Type: {bloodType}      Rh: {bloodRh}",
                    TestOrderName = "Blood Typing",
                    TestOrderTypeId = TestOrderTypeEnum.BloodTyping
                });
            }

            if (order.BloodScreeningTestOrder != null)
            {
                results.Add(new RequestedTestOrderDto()
                {
                    Result = order.BloodScreeningTestOrder.Result,
                    TestOrderName = "Blood Screening",
                    TestOrderTypeId = TestOrderTypeEnum.BloodScreening
                });
            }

            return results;
        }

        public async Task<bool> GetTestOrderStatus(Guid reservationId)
        {
            var testOrder = await repository.TestOrder.FindByConditionAsync(x => x.ReservationId == reservationId);

            if (testOrder == null)
            {
                throw new RecordNotFoundException($"Record not found on test order for reservation id: {reservationId}");
            }

            return testOrder.All(x => x.TestCompleted == true);
        }
    }
}
