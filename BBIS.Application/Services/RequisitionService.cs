using AutoMapper;
using BBIS.Application.ConnectionProvider;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Requisition;
using BBIS.Application.DTOs.TestOrder;
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
    public class RequisitionService : IRequisitionService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly IConnectionProvider dbConnection;

        public RequisitionService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper, IConnectionProvider connection)
        {
            this.dbConnection = connection;
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateReservation(ReservationDto dto, Guid userId)
        {

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(ReservationDto));
            }

            // Add the reservation
            var reservation = new Reservation {
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
                ForAdult = dto.ForAdult,
                IsEmergency = dto.IsEmergency,
                Membership = dto.Membership,
                PatientId = dto.PatientId,
                PatientType = dto.PatientType,
                PreviousNoOfUnitsTransfused = dto.PreviousNoOfUnitsTransfused,
                PreviousTransfusionDate = dto.PreviousTransfusionDate,
                PriorityLevel = dto.PriorityLevel,
                RequestedBy = dto.RequestedBy,
                RequestedDateTime = dto.RequestedDateTime,
                RoomNo = dto.RoomNo,
                Status = RequisitionStatus.Received,
                Expiration = DateTime.UtcNow.AddHours(24)
            };
           
            // Add Reservation Items
            if (dto.ReservationItems.Any())
            {
                reservation.ReservationItems = new List<ReservationItem>();

                foreach (var item in dto.ReservationItems)
                {
                    var inventoryItem = await repository.InventoryItem.FindOneByConditionAsync(x => x.InventoryItemId == item.InventoryItemId);
                    if (inventoryItem == null)
                    {
                        throw new RecordNotFoundException($"Inventory not found for Id: {item.InventoryItemId}");
                    }

                    // Set the status to Reserved
                    inventoryItem.Status = InventoryStatus.Reserved;
                    repository.InventoryItem.Update(inventoryItem);

                    var reservationItem = new ReservationItem
                    {
                        BloodComponentId = item.BloodComponentId,
                        InventoryItemId = item.InventoryItemId,
                        Volume = inventoryItem.Volume,
                        OtherNotes = item.OtherNotes
                    };

                    var query = await dbContext.InventorySources
                       .Include(x => x.DonorTransaction)
                       .FirstOrDefaultAsync(x => x.InventorySourceId == inventoryItem.InventorySourceId);

                    // Set the DonorId
                    if (query != null && query.DonorTransaction != null) 
                    {
                        reservationItem.DonorTransactionId = query.DonorTransaction.DonorTransactionId;
                        reservationItem.DonorUnitSerialNumber = query.DonorTransaction.SegmentSerialNumber;
                    }

                    if (item.ReservationCheckLists.Any())
                    {
                        reservationItem.ReservationChecklists = new List<ReservationChecklist>();

                        foreach (var checkListId in item.ReservationCheckLists)
                        {
                            var reservationCheckList = new ReservationChecklist { BloodComponentChecklistId = checkListId };

                            reservationItem.ReservationChecklists.Add(reservationCheckList);
                        }
                    }

                    reservation.ReservationItems.Add(reservationItem);
                }
            }

            repository.Reservation.Create(reservation);

            await repository.SaveAsync();

            return reservation.ReservationId;
        }

        public async Task<Guid> UpdateReservation(UpdateReservationDto dto, Guid userId)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(UpdateReservationDto));
            }

            var reservation = await dbContext.Reservations
                .Include(x => x.ReservationItems)
                .Include(x => x.TestOrders)
                    .ThenInclude(x => x.CrossMatchTestOrders)
                .FirstOrDefaultAsync(x => x.ReservationId == dto.ReservationId);
            if (reservation == null) throw new RecordNotFoundException($"Reservation does not exist for Id: {dto.ReservationId}");

            reservation.Status = dto.Status;
            reservation.UpdatedBy = userId;
            reservation.UpdatedDate = DateTime.UtcNow;

            if (dto.Status == RequisitionStatus.Received || dto.Status == RequisitionStatus.InProgress)
            {
                foreach (var item in reservation.ReservationItems)
                {
                    var inventory = await repository.InventoryItem.FindOneByConditionAsync(x => x.InventoryItemId == item.InventoryItemId);
                    inventory.Status = InventoryStatus.Reserved;

                    repository.InventoryItem.Update(inventory);
                }
            }

            if (dto.Status == RequisitionStatus.Cancelled)
            {
                foreach (var item in reservation.ReservationItems)
                {
                    var inventory = await repository.InventoryItem.FindOneByConditionAsync(x => x.InventoryItemId == item.InventoryItemId);
                    inventory.Status = InventoryStatus.InStock;

                    repository.InventoryItem.Update(inventory);
                }
            }

            if (dto.Status == RequisitionStatus.ForTransfusion || dto.Status == RequisitionStatus.Done)
            {
                // Make sure the test order for compatibility exist
                var testOrder = reservation.TestOrders.FirstOrDefault(x => x.TestCompleted && x.CrossMatchTestOrders.Any());
                   
                if (testOrder != null && testOrder.CrossMatchTestOrders.Any())
                {
                    foreach (var item in reservation.ReservationItems)
                    {
                        // Check if the cross match result is not Incompatible, otherwise inventory item will not be mark as Acquired
                        var xmatch = testOrder.CrossMatchTestOrders.FirstOrDefault(x => x.DonorTransactionId == item.DonorTransactionId && x.BloodComponentId == item.BloodComponentId);

                        if (xmatch.Result != CompatibilityOptions.Incompatible)
                        {
                            var inventory = await repository.InventoryItem.FindOneByConditionAsync(x => x.InventoryItemId == item.InventoryItemId);
                            inventory.Status = InventoryStatus.Acquired;

                            repository.InventoryItem.Update(inventory);
                        }
                    }
                }
            }

            repository.Reservation.Update(reservation);

            await repository.SaveAsync();

            return reservation.ReservationId;
        }

        public async Task<List<CheckListOptionDto>> GetReservationChecklist()
        {
            var query = await dbContext.BloodComponents
                .Include(x => x.BloodComponentChecklists.Where(x => x.IsActive))
                .Where(x => x.IsActive)
                .AsNoTracking()
                .Select(x =>
                    new CheckListOptionDto
                    {
                        BloodComponentId = x.BloodComponentId,
                        ComponentName = x.ComponentName,
                        CheckLists = x.BloodComponentChecklists
                           .Select(c => 
                                new CheckListDto { 
                                    ChecklistId = c.BloodComponentChecklistId,
                                    Description = c.ChecklistDescription,
                                    IsAdult = c.IsAdult
                                }).ToList()
                    })
                .ToListAsync();

            return query;
        }

        public async Task<PagedSearchResultDto<ReservationListingDto>> GetReservations(RequisitionPagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(RequisitionPagedSearchDto));
            }

            if (searchDto.Statuses == null || !searchDto.Statuses.Any())
            {
                searchDto.Statuses = new List<string>() { RequisitionStatus.Received, RequisitionStatus.InProgress, RequisitionStatus.ForTransfusion, RequisitionStatus.Done};
            }

            searchDto.DateFrom = searchDto.DateFrom.ConvertFilterToUtcStartOfDay();
            searchDto.DateTo = searchDto.DateTo.ConvertFilterToUtcEndOfDay();

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "PatientName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<ReservationListingDto>(searchDto);

            var query = dbContext.Reservations
                .Include(x => x.ReservationItems)
                    .ThenInclude(x => x.BloodComponent)
                .Include(x => x.TestOrders)
                    .ThenInclude(x => x.CrossMatchTestOrders)
                .Include(x => x.TestOrders)
                    .ThenInclude(x => x.BloodTypingTestOrder)
                .Include(x => x.TestOrders)
                    .ThenInclude(x => x.BloodScreeningTestOrder)
                .Include(x => x.TestOrders)
                    .ThenInclude(x => x.CoombsTestOrder)
                .Include(x => x.Patient)
                .Where(x => (searchDto.DateFrom == null || searchDto.DateTo == null || x.RequestedDateTime >= searchDto.DateFrom.Value && x.RequestedDateTime <= searchDto.DateTo.Value) && searchDto.Statuses.Contains(x.Status))
                .Search(t => t.Patient.FirstName, t => t.Patient.LastName, t => t.Patient.MiddleName)
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
                    new ReservationListingDto
                    {
                        ReservationId = x.ReservationId,
                        PatientId = x.PatientId,
                        PatientName = $"{x.Patient.FirstName} {x.Patient.MiddleName.Substring(0, 1)}. {x.Patient.LastName}",
                        Age = x.Patient.DateOfBirth.CalculateAge(),
                        BloodType = x.Patient.BloodType + (x.Patient.Rh.ToLower() == "positive" ? "+" : (x.Patient.Rh.ToLower() == "negative" ? "-" : "") ),
                        RequestedDate = x.RequestedDateTime,
                        Components = x.ReservationItems.GroupBy(g => g.BloodComponentId)
                            .Select(g => $"{g.First().BloodComponent.ComponentName} - ({g.Count()})" ).ToList(),
                        TestOrderSummary = x.TestOrders.Select( x => new RequestedTestOrderSummaryDto
                        {
                            TestOrderId= x.TestOrderId,
                            CrossMatchTestOrders = x.CrossMatchTestOrders
                                .GroupBy(g => g.CrossMatchType)
                                .Select(x => $"Cross Match - {x.First().CrossMatchType} - ({x.Count()})")
                                .ToList(),
                            TestCompleted = x.TestCompleted,
                            TestOrders = new List<string>() {
                                x.BloodTypingTestOrder != null ? "Blood Typing" : "",
                                x.BloodScreeningTestOrder != null ? "Blood Screening" : "",
                                x.CoombsTestOrder != null ? "Coomb Test" : "",
                            }.ToList()
                        }).ToList(),
                        Status = x.Status
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

        public async Task<TransfusionViewDetailsDto> GetTransfusionDetails(Guid reservationId)
        {
            var dto = new TransfusionViewDetailsDto();

            var query = await dbContext.Reservations
                .Include(x => x.Patient)
                .Include(x => x.ReservationItems.Where(x => x.InventoryItem.Status == InventoryStatus.Acquired))
                    .ThenInclude(x => x.InventoryItem)
                        .ThenInclude(x => x.BloodComponent)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ReservationId == reservationId);

            if (query == null)
            {
                throw new RecordNotFoundException("Reservation not found");
            }

            var patientDetails = new PatientDetailsDto { 
                FullName = $"{query.Patient.FirstName} {query.Patient.MiddleName.Substring(0, 1)}. {query.Patient.LastName}",
                Age = query.Patient.DateOfBirth.CalculateAge(),
                Gender = query.Patient.Gender,
                BloodType = query.Patient.BloodType,
                BloodRh = query.Patient.Rh,
                PatientNo = query.Patient.PatientIdNo,
                RequestingPhysician = query.RequestedBy,
                RoomNo = query.RoomNo,
                Membership = query.Membership,
                PreviousTransfusionDate = query.PreviousTransfusionDate,
                PreviousNoOfUnitsTransfused = query.PreviousNoOfUnitsTransfused
            };

            var bloodComponents = new List<BloodComponentDetailsDto>();

            foreach (var item in query.ReservationItems)
            {
                var details = new BloodComponentDetailsDto
                {
                    ReservationItemId = item.ReservationItemId,
                    ComponentName = item.InventoryItem.BloodComponent.ComponentName,
                    ComponentBloodType = item.InventoryItem.BloodType,
                    ComponentBloodRh = item.InventoryItem.BloodRh,
                    Volume = item.InventoryItem.Volume,
                    UnitMeasure = item.InventoryItem.UnitMeasure,
                    UnitSerialNumber = item.InventoryItem.UnitSerialNumber,
                    ExpirationDate = item.InventoryItem.ExpiryDate,
                    Tranfusion = await GetTransfusion(item.ReservationItemId)
                };

                bloodComponents.Add(details);
            }

            dto.ReservationId = reservationId;
            dto.ReservationStatus = query.Status;
            dto.PatientDetails = patientDetails;
            dto.BloodComponentDetails = bloodComponents;
            return dto;
        }

        public async Task<Guid> UpsertTransfusionDetails(List<TransfusionDto> transfusionDto)
        {
            if (transfusionDto == null)
            {
                throw new ArgumentNullException(nameof(TransfusionViewDetailsDto));
            }

            if (transfusionDto.Any())
            {
                foreach (var item in transfusionDto)
                {
                    var transfusion = mapper.Map<Transfusion>(item);
                    transfusion.TransfusionVitalSigns = mapper.Map<List<TransfusionVitalSign>>(item.VitalSigns);

                    if (item.TransfusionId == null)
                    {
                        repository.Transfusion.Create(transfusion);
                    }
                    else
                    {
                        repository.Transfusion.Update(transfusion);
                    }
                }

                await repository.SaveAsync();
            }

            return transfusionDto.FirstOrDefault().ReservationId;
        }

        private async Task<TransfusionDto> GetTransfusion(Guid reservationItemId)
        {
            var transfusion = await dbContext.Transfusions
                .Include(x => x.TransfusionVitalSigns)
                .Where(x => x.ReservationItemId == reservationItemId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (transfusion == null)
            {
                return new TransfusionDto
                {
                    VitalSigns = new List<TransfusionVitalSignDto>
                    {
                        new TransfusionVitalSignDto
                        {
                            VitalSignType = VitalSignType.PreTransfusion
                        },
                        new TransfusionVitalSignDto
                        {
                            VitalSignType = VitalSignType.DuringTransfusion
                        },
                        new TransfusionVitalSignDto
                        {
                            VitalSignType = VitalSignType.PostTransfusion
                        }
                    }
                };
            }

            var vitalSigns = mapper.Map<List<TransfusionVitalSignDto>>(transfusion.TransfusionVitalSigns);
            var dto = mapper.Map<TransfusionDto>(transfusion);

            dto.VitalSigns = vitalSigns;
            return dto;
        }

        public async Task<List<GroupCrossMatchOrderDto>> GetReservationMatchingDonors(Guid reservationId)
        {
            var reservationItems = await dbContext.ReservationItems
                    .Include(x => x.BloodComponent)
                    .Include(x => x.DonorTransaction).ThenInclude(x => x.DonorBloodCollection)
                    .Include(x => x.InventoryItem).ThenInclude(x => x.InventorySource)
                    .Where(x => x.ReservationId == reservationId)
                    .ToListAsync();

            if (reservationItems == null)
            {
                throw new RecordNotFoundException($"Reservation not found for Id: {reservationId}");
            }

            var appSetting = await dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.SettingKey == ApplicationSettingKeys.InstitutionName);

            var bloodComponents = reservationItems
                .GroupBy(x => x.BloodComponentId).Select(x => x.First())
                .Select(s => s.BloodComponentId).ToList();

            var results = new List<GroupCrossMatchOrderDto>();

            foreach (var componentId in bloodComponents)
            {
                var items = reservationItems.Where(x => x.BloodComponentId == componentId);

                var group = new GroupCrossMatchOrderDto() {
                    BloodComponentId = componentId,
                    BloodComponentName = items.FirstOrDefault().BloodComponent.ComponentName,
                    CrossMatchTestOrders = items.Select(x => new CreateCrossMatchOrderDto() {
                        BloodComponentId = x.BloodComponentId,
                        BloodComponentName = x.BloodComponent.ComponentName,
                        CollectionDate = x.DonorTransaction.DonorBloodCollection.EndTime,
                        CrossMatchType = "",
                        DonorTransactionId = x.DonorTransactionId,
                        DonorUnitSerialNumber = x.DonorUnitSerialNumber,
                        ExpiryDate = x.InventoryItem.ExpiryDate,
                        Result = "",
                        Source = "",
                        Volume = x.InventoryItem.Volume
                    }).ToList()
                };

                results.Add(group);
            }

            return results;
        }
    }
}
