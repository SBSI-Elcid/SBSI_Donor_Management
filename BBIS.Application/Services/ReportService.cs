using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Reports;
using BBIS.Common.Enums;
using BBIS.Database;
using Dapper;
using System.Data;
using System.Linq.Dynamic.Core;
using BBIS.Application.ConnectionProvider;
using BBIS.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using BBIS.Common.Exceptions;
using BBIS.Application.DTOs.TestOrder;
using BBIS.Application.Extension;
using BBIS.Common.Extensions;

namespace BBIS.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly BBDbContext dbContext;
        private readonly IConnectionProvider dbConnection;
        private readonly IRepositoryWrapper repository;

        public ReportService(BBDbContext dbContext, IConnectionProvider connection, IRepositoryWrapper repository)
        {
            this.dbContext = dbContext;
            this.dbConnection = connection;
            this.repository = repository;
        }

        public async Task<List<DonorReportDto>> GetDonorReport(DonorReportFiltersDto filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(DonorReportDto));
            }

            if (filters.Statuses == null || !filters.Statuses.Any())
            {
                filters.Statuses = new List<string>() { DonorStatus.Success, DonorStatus.Inventory, DonorStatus.Deferred };
            }
            else if (filters.Statuses.Any(x => x.Equals(DonorStatus.Success)))
            {
                // If user is filtering Success in status filter, include those that are in "Inventory" status as these are all success
                filters.Statuses.Add(DonorStatus.Inventory);
            }

            if (filters.BloodTypes == null || !filters.BloodTypes.Any())
            {
                filters.BloodTypes = new List<string>() { "A", "O", "B", "AB" };
            }

            var status_list = string.Join(",", filters.Statuses);
            var bloodtype_list = string.Join(",", filters.BloodTypes);
            string date_from = null;
            string date_to = null;

            if (filters.DonationDateFrom != null && filters.DonationDateTo != null)
            {
                date_from = filters.DonationDateFrom.ConvertFilterToUtcStartOfDay().Value.ToString("yyyy-MM-dd");
                date_to = filters.DonationDateTo.ConvertFilterToUtcStartOfDay().Value.ToString("yyyy-MM-dd");
            }

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@status_list", status_list);
            queryParameters.Add("@bloodtype_list", bloodtype_list);
            queryParameters.Add("@date_from", date_from);
            queryParameters.Add("@date_to", date_to);

            using var connection = dbConnection.GetDbConnection();
            var results = await connection.QueryAsync<DonorReportDto>("sp_GetDonorReport", queryParameters, commandType: CommandType.StoredProcedure, commandTimeout: 60);

            return results.ToList();
        }

        public async Task<ExportReportDto<DonorReportDto>> GetDonorReportForExport(DonorReportFiltersDto filters)
        {
            var results = await GetDonorReport(filters);

            // Set the DateTime.Kind to UTC as the date time saved in the database is in UTC. This would ensure a correct date when exporting data to excel.
            results.ForEach(x => x.DateOfDonation = DateTime.SpecifyKind(x.DateOfDonation, DateTimeKind.Utc));

            if (results == null)
            {
                return new ExportReportDto<DonorReportDto>();
            }

            var excelColumns = new List<Columns>
            {
                new Columns
                {
                    Label =  "Unit Serial No.",
                    Field = "UnitSerialNumber"
                },
                new Columns
                {
                    Label =  "Gender",
                    Field = "Gender"
                },
                new Columns
                {
                    Label =  "Age",
                    Field = "Age"
                },
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
                    Label =  "Collected Amount",
                    Field = "CollectedAmount"
                },
                new Columns
                {
                    Label =  "Donation Date",
                    Field = "DateOfDonation"
                },
                new Columns
                {
                    Label =  "Status",
                    Field = "Status"
                }
            };

            return new ExportReportDto<DonorReportDto>
            {
                Columns = excelColumns,
                Rows = results
            };
        }

        public async Task<TestOrderReportDto> GetTestOrderReport(Guid id)
        {
            var testOrder = await dbContext.TestOrders
                .Include(x => x.Patient)
                .Include(x => x.CrossMatchTestOrders).ThenInclude(x => x.BloodComponent)
                .Include(x => x.BloodScreeningTestOrder)
                .Include(x => x.BloodTypingTestOrder)
                .Include(x => x.CoombsTestOrder)
                .FirstOrDefaultAsync(x => x.TestOrderId == id);

            if (testOrder == null)
            {
                throw new RecordNotFoundException();
            }

            var appSetting = await dbContext.ApplicationSettings.FirstAsync(x => x.SettingKey == ApplicationSettingKeys.InstitutionName);

            var result = new TestOrderReportDto
            {
                Remarks = testOrder.Remarks,
                InstitutionName = appSetting.SettingValue,
                TestOrderNumber = testOrder.TestOrderNumber,
                BloodType = $"{testOrder.Patient.BloodType} {testOrder.Patient.Rh}",
                CivilStatus = testOrder.Patient.CivilStatus,
                Gender= testOrder.Patient.Gender,
                PatientIdNo= testOrder.Patient.PatientIdNo,
                PatientName = $"{testOrder.Patient.FirstName} {testOrder.Patient.MiddleName.Substring(0,1)}. {testOrder.Patient.LastName}",
                TestCompleted = testOrder.TestCompleted ? "Yes" : "No",
                DateCreated = testOrder.CreatedDate != null ? testOrder.CreatedDate.Value.ToString("dd MMMM yyyy") : "",
                CrossMatchTestOrders = testOrder.CrossMatchTestOrders.Select(x => new CrossMatchTestOrderDto()
                {
                    BloodComponentId = x.BloodComponentId,
                    BloodComponentName = x.BloodComponent.ComponentName,
                    CrossMatchType = x.CrossMatchType,
                    DonorTransactionId = x.DonorTransactionId,
                    DonorUnitSerialNumber = x.DonorUnitSerialNumber,
                    Result = x.Result,
                    Source = x.Source,
                    LISS_AGH = x.LISS_AGH,
                    CrossMatchTestOrderId = x.CrossMatchTestOrderId,
                    ExpiryDate = x.ExpiryDate,
                    CollectionDate = x.CollectionDate
                }).ToList()
            };

            var performedBy = await dbContext.Signatories.FirstOrDefaultAsync(x => x.SignatoryId == testOrder.PerformedBy);
            if(performedBy != null)
            {
                result.PerformedBy = performedBy.GetSignatoryReportDto();
            }

            var reviewedBy = await dbContext.Signatories.FirstOrDefaultAsync(x => x.SignatoryId == testOrder.ReviewedBy);
            if (reviewedBy != null)
            {
                result.ReviewedBy = reviewedBy.GetSignatoryReportDto();
            }

            var validatedBy = await dbContext.Signatories.FirstOrDefaultAsync(x => x.SignatoryId == testOrder.ValidatedBy);
            if (validatedBy != null)
            {
                result.ValidatedBy = validatedBy.GetSignatoryReportDto();
            }

            var pathologist = await dbContext.Signatories.FirstOrDefaultAsync(x => x.SignatoryId == testOrder.PhatologistId);
            if (pathologist != null)
            {
                result.Phatologist = pathologist.GetSignatoryReportDto();
            }
                       
            if (testOrder.BloodTypingTestOrder != null)
            {
                var order = testOrder.BloodTypingTestOrder;
                result.BloodTypingTestOrder = new BloodTypingTestOrderDto()
                {
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

            if (testOrder.BloodScreeningTestOrder != null)
            {
                var order = testOrder.BloodScreeningTestOrder;
                result.BloodScreeningTestOrder = new BloodScreeningTestOrderDto()
                {
                    Result = order.Result,
                    BloodScreeningTestOrderId = order.BloodScreeningTestOrderId
                };
            }

            if (testOrder.CoombsTestOrder != null)
            {
                var order = testOrder.CoombsTestOrder;
                result.CoombsTestOrder = new CoombsTestOrderDto()
                {
                    CoombsTestOrderId = order.CoombsTestOrderId,
                    DATResult = order.DATResult,
                    IATResult = order.IATResult
                };
            }
                      
            return result;
        }
    }
}
