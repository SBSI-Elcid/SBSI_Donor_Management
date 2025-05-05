using AutoMapper;
using BBIS.Application.ConnectionProvider;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Inventory;
using BBIS.Application.DTOs.Reports;
using BBIS.Common.Enums;
using BBIS.Common.Exceptions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;

namespace BBIS.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly IConnectionProvider dbConnection;

        public InventoryService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper, IConnectionProvider connection)
        {
            this.dbConnection = connection;
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddToInventory(AddInventoryDto dto, Guid userId)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(AddInventoryDto));
            }

            if(!dto.DonorTranctionId.HasValue && !dto.InstitutionId.HasValue)
            {
                throw new ArgumentNullException("Inventory source is not set");
            }

            if(dto.DonorTranctionId.HasValue)
            {
                // Make sure transaction exist and Status = Success
                var donorTransaction = await dbContext.DonorTransactions
                    .FirstOrDefaultAsync(x => x.DonorTransactionId == dto.DonorTranctionId && x.DonorStatus == DonorStatus.Success);

                if (donorTransaction == null)
                {
                    throw new RecordNotFoundException($"Donor transaction not found for Id: {dto.DonorTranctionId}, Status: {DonorStatus.Success}");
                }

                // Make sure transaction does not exists in inventory
                var inventorySource = await repository.InventorySource.FindOneByConditionAsync(x => x.DonorTranctionId == dto.DonorTranctionId);
                if(inventorySource != null)
                {
                    throw new RecordExistException($"Inventory already exist for transactionId: {dto.DonorTranctionId}");
                }

                donorTransaction.DonorStatus = DonorStatus.Inventory;
               
                repository.DonorTransaction.Update(donorTransaction);
            }

            if (dto.InstitutionId.HasValue)
            {
                // Make sure InstitutionId exists
                var institution = await repository.Institution.FindOneByConditionAsync(x => x.InstitutionId == dto.InstitutionId);
                if (institution == null)
                {
                    throw new RecordNotFoundException($"Institution not found for Id: {dto.InstitutionId}");
                }
            }

            var newInventorySource = new InventorySource
            {
                DonorTranctionId = dto.DonorTranctionId,
                InstitutionId = dto.InstitutionId,
                IsExternalSource = dto.InstitutionId.HasValue,
                InventoryItems = new List<InventoryItem>()
            };

            foreach(var item in dto.InventoryItems)
            {
                var newItem = new InventoryItem
                {
                  AddedById  = userId,
                  BloodComponentId = item.BloodComponentId,
                  BloodType = item.BloodType,
                  BloodRh = item.BloodRh,
                  CollectionDate = item.CollectionDate,
                  DateAdded = DateTime.UtcNow,
                  ExpiryDate = item.ExpiryDate,
                  NotifyBeforeExpireOn = item.NotifyBeforeExpireOn,
                  Status = InventoryStatus.InStock,
                  UnitMeasure = item.UnitMeasure,
                  UnitSerialNumber = item.UnitSerialNumber,
                  Volume = item.Volume 
                };

                newInventorySource.InventoryItems.Add(newItem);
            }

            repository.InventorySource.Create(newInventorySource);

            await repository.SaveAsync();
        }

        public async Task<List<InventoryListDto>> GetInventoryItems(InventoryFilterDto filter)
        {
            if(filter == null)
            {
                throw new ArgumentNullException(nameof(InventoryFilterDto));
            }

            if(filter.Statuses == null || !filter.Statuses.Any())
            {
                filter.Statuses = new List<string>() { InventoryStatus.InStock, InventoryStatus.NearExpiry, InventoryStatus.Reserved };
            }

            if (filter.BloodTypes == null || !filter.BloodTypes.Any())
            {
                filter.BloodTypes = new List<string>() { "A+", "A-", "O+", "O-", "B+", "B-", "AB+", "AB-" };
            }

            var status_list = string.Join(",", filter.Statuses);
            var bloodtype_list = string.Join(",", filter.BloodTypes);
            var datefilter_type = filter.DateFilterType;
            var date_from = "";
            var date_to = "";

            if(!string.IsNullOrEmpty(filter.DateFilterType) && filter.DateFrom != null && filter.DateTo != null)
            {
                date_from = filter.DateFrom.Value.ToString("yyyy-MM-dd");
                date_to = filter.DateTo.Value.ToString("yyyy-MM-dd");
            }

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@status_list", status_list);
            queryParameters.Add("@bloodtype_list", bloodtype_list);
            queryParameters.Add("@datefilter_type", datefilter_type);
            queryParameters.Add("@date_from", date_from);
            queryParameters.Add("@date_to", date_to);

            //var projectList = await dataAccess.RunStoredProcAsync<ProjectListResult>("Sproc_ProjectList", queryParameters);
            using var connection = dbConnection.GetDbConnection();
            var results = await connection.QueryAsync<InventoryListDto>("sp_GetInventoryItems", queryParameters, commandType: CommandType.StoredProcedure, commandTimeout: 60);

            return results.ToList();
        }

        public async Task<ExportReportDto<InventoryListDto>> GetInventoryItemsForExport(InventoryFilterDto filter)
        {
            var results = await GetInventoryItems(filter);

            if (results == null)
            {
                return new ExportReportDto<InventoryListDto>();
            }

            var excelColumns = new List<Columns>
            {
                new Columns
                {
                    Label =  "Blood Type",
                    Field = "BloodType"
                },                
                new Columns
                {
                    Label =  "Blood Rh",
                    Field = "BloodRh"
                },
                new Columns
                {
                    Label =  "Component",
                    Field = "ComponentName"
                },
                new Columns
                {
                    Label =  "Unit Serial No.",
                    Field = "UnitSerialNumber"
                },
                new Columns
                {
                    Label =  "No. Of Unit",
                    Field = "NoOfUnit"
                },
                new Columns
                {
                    Label =  "Collected By",
                    Field = "CollectedBy"
                },
                new Columns
                {
                    Label =  "Collection Date",
                    Field = "CollectionDate"
                },
                new Columns
                {
                    Label =  "Expiration Date",
                    Field = "ExpiryDate"
                },
                new Columns
                {
                    Label =  "Status",
                    Field = "Status"
                }
            };

            return new ExportReportDto<InventoryListDto>
            {
                Columns = excelColumns,
                Rows = results
            };
        }

        public async Task<List<InventoryGroupDto>> GetInventoryStatus()
        {
            var results = new List<InventoryGroupDto>();

            var bloodTypes = new List<(string type, string rh)> { 
                (BloodTypes.A, "Positive"),
                (BloodTypes.A, "Negative"),
                (BloodTypes.O, "Positive"),
                (BloodTypes.O, "Negative"),
                (BloodTypes.AB, "Positive"),
                (BloodTypes.AB, "Negative"),
                (BloodTypes.B, "Positive"),
                (BloodTypes.B, "Negative")
            };
            var excludeStatus = new List<string>() { InventoryStatus.Acquired, InventoryStatus.Expired, InventoryStatus.Reserved };
            
            foreach(var bloodType in bloodTypes)
            {
                var group = new InventoryGroupDto() { BloodType = bloodType.type, BloodRh = bloodType.rh };
                // Get the expired count this month only
                group.TotalExpiredCount = await dbContext.InventoryItems.CountAsync(x => 
                    x.BloodType == bloodType.type &&
                    x.BloodRh == bloodType.rh &&
                    x.Status == InventoryStatus.Expired && 
                   (x.ExpiryDate.Month == DateTime.Now.Month && x.ExpiryDate.Year == DateTime.Now.Year));

                var query = await dbContext.InventoryItems
                    .Include(x => x.BloodComponent)
                    .Where(x => x.BloodType == bloodType.type && x.BloodRh == bloodType.rh && !excludeStatus.Contains(x.Status))
                    .AsNoTracking()
                    .OrderByDescending(x => x.ExpiryDate)
                    .Take(10)
                    .ToListAsync();

                var items = query
                    .Select(x =>
                        new InventoryViewDto
                        {
                            CollectionDate = x.CollectionDate.ToString("MM-dd-yyyy"),
                            Component = x.BloodComponent.ComponentName,
                            ExpiryDate = x.ExpiryDate.ToString("MM-dd-yyyy"),
                            InventoryItemId = x.InventoryItemId,
                            NoOfUnit = $"{x.Volume}{x.UnitMeasure}",
                            Status = x.Status
                        }).ToList();

                group.InventoryItems = items;
                group.TotalNumberOfUnits = query.Where(x => x.Status == InventoryStatus.InStock || x.Status == InventoryStatus.NearExpiry).Sum(s => s.Volume);

                results.Add(group);
            }
    
            return results.OrderBy(x => x.BloodType).ToList();
        }

        public async Task<List<InventoryItemDto>> PrepareInventoryItems(Guid transactionId)
        {
            var donorTransaction = await dbContext.DonorTransactions
                .Include(x => x.DonorBloodCollection)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DonorTransactionId == transactionId && x.DonorStatus == DonorStatus.Success);

            if (donorTransaction == null)
            {
                throw new RecordNotFoundException($"Donor transaction not found for Id: {transactionId}");
            }

            var bloodComponents = await dbContext.BloodComponents.Where(x => x.IsActive).ToListAsync();
            var appSetting = await dbContext.ApplicationSettings.FirstOrDefaultAsync(x => x.IsActive && x.SettingKey == ApplicationSettingKeys.BloodComponentsUnitOfMeasure);

            var collectionDate = donorTransaction.DonorBloodCollection.StartTime.Date;
            var unitMeasure = appSetting != null ? appSetting.SettingValue : "mL";

            var inventoryItems = new List<InventoryItemDto>();

            foreach (var bloodComponent in bloodComponents)
            {
                var expiryDate = collectionDate.AddDays(bloodComponent.ExpiryInDays);
                var startNotifyExpiryOn = expiryDate.AddDays(bloodComponent.NotifyOnDaysBefore * (-1));

                var newItem = new InventoryItemDto()
                {
                    BloodComponentId = bloodComponent.BloodComponentId,
                    BloodComponentName = bloodComponent.ComponentName,
                    BloodType = donorTransaction.FinalBloodType,
                    BloodRh = donorTransaction.BloodRh,
                    CollectionDate = donorTransaction.DonorBloodCollection.StartTime.Date,
                    ExpiryDate = expiryDate,
                    NotifyBeforeExpireOn = startNotifyExpiryOn,
                    UnitMeasure = unitMeasure,
                    UnitSerialNumber = donorTransaction.UnitSerialNumber,
                    Volume = 0
                };

                inventoryItems.Add(newItem);
            }

            return inventoryItems;
        }

        public async Task<List<AvailableInventoryItemDto>> GetAvailableInventoryUnits(string bloodType, string bloodRh)
        {
            var statuses = new List<string>() { InventoryStatus.Expired, InventoryStatus.Reserved, InventoryStatus.Acquired };

            var query = await dbContext.InventoryItems
                .Include(x => x.InventorySource)
                    .ThenInclude(x => x.DonorTransaction)                    
                .Include(x => x.BloodComponent)
                .Where(x => x.BloodType == bloodType && x.BloodRh.ToLower() == bloodRh.ToLower() && !statuses.Contains(x.Status))
                .OrderBy(x => x.ExpiryDate)
                .Select(x => new 
                {
                    x.BloodComponentId,
                    x.InventoryItemId,
                    x.Volume,
                    x.UnitMeasure,
                    x.ExpiryDate,
                    x.InventorySource.DonorTransaction.UnitSerialNumber,
                    x.BloodType, 
                    x.BloodRh,
                })
                .Distinct()
                .ToListAsync();

            var groupByComponents = query.GroupBy(x => x.BloodComponentId);
            var options = groupByComponents.Select(x => new AvailableInventoryItemDto
            {
                BloodComponentId = x.Key,
                AvailableOptions = x.Select(c =>
                                new InventoryUnitOptionsDto
                                {
                                    InventoryItemId = c.InventoryItemId,
                                    ItemText = $"{c.UnitSerialNumber} - {c.Volume}{c.UnitMeasure} ({c.BloodType}{ToRhSign(c.BloodRh)})" 
                                }).ToList()
            }).ToList();

            return options;
        }

        private static string ToRhSign(string rh)
        {
            return rh.ToLower() == "positive" ? "+" : "-";
        }
    }
}
