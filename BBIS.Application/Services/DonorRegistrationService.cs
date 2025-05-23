using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Common.Enums;
using BBIS.Common.Exceptions;
using BBIS.Database;
using BBIS.Domain.Contracts;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;

namespace BBIS.Application.Services
{
    public class DonorRegistrationService : IDonorRegistrationService
    {
        private readonly BBDbContext dbContext;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public DonorRegistrationService(BBDbContext dbContext, IRepositoryWrapper repository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<MedicalQuestionnaireDto>> GetMedicalQuestionnaires()
        {
            var questionnaires = await dbContext.MedicalQuestionnaires.ToListAsync();
            var results = questionnaires
                    .Select(x => new MedicalQuestionnaireDto
                    {
                        HeaderText = x.HeaderText,
                        MedicalQuestionnaireId = x.MedicalQuestionnaireId,
                        OrderNo = x.OrderNo,
                        QuestionEnglishText = x.QuestionEnglishText,
                        QuestionTagalogText = x.QuestionTagalogText,
                        GenderOption = x.GenderOption
                    })
                    .OrderBy(x => x.OrderNo)
                    .ToList();

            return results;
        }

        public async Task<PagedSearchResultDto<RegisteredDonorDto>> GetRegisteredDonors(PagedSearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(PagedSearchDto));
            }

            var sortBy = string.IsNullOrEmpty(searchDto.SortBy) ? "FullName asc" : searchDto.SortBy + (searchDto.SortDesc ? " desc" : " asc");
            var pagedResult = new PagedSearchResultDto<RegisteredDonorDto>(searchDto);

            var searchText = searchDto.SearchText;

            var query = dbContext.DonorRegistrations
                .Include(x => x.DonorTransactions)
                .Where(x => !x.DonorTransactions.Any())
                .Search(t => t.Firstname, t => t.Lastname, t => t.Middlename, t => t.RegistrationNumber)
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
                        new RegisteredDonorDto
                        {
                            DonorRegistrationId = x.DonorRegistrationId,
                            RegistrationNumber = x.RegistrationNumber,
                            FullName = $"{x.Firstname} {x.Middlename.Substring(0, 1)}. {x.Lastname}",
                            CivilStatus = x.CivilStatus,
                            Gender = x.Gender,
                            RegistrationDate = x.RegistrationDate
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

        public async Task<RegisteredDonorInfoDto> GetRegisteredDonorInfo(Guid regId)
        {
            var dto = new RegisteredDonorInfoDto();

            var donorRegistration = await dbContext.DonorRegistrations
               .Include(x => x.DonorMedicalHistories)
               .ThenInclude(x => x.MedicalQuestionnaire)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.DonorRegistrationId == regId);

            if (donorRegistration == null)
            {
                throw new RecordNotFoundException("Donor Registration information was not found");
            }

            dto = mapper.Map<RegisteredDonorInfoDto>(donorRegistration);
            dto.BirthDate = DateTime.SpecifyKind(dto.BirthDate, DateTimeKind.Utc);

            // Check if there's already associated transaction record
            var donorTransaction = await repository.DonorTransaction.FindOneByConditionAsync(x => x.DonorRegistrationId == regId);
            if (donorTransaction != null)
            {
                dto.DonorStatus = donorTransaction.DonorStatus;
                dto.DonorTransactionId = donorTransaction.DonorTransactionId;
            }
            else
            {
                dto.DonorStatus = DonorStatus.ForInitialScreening;
            }

            dto.MedicalHistories = donorRegistration.DonorMedicalHistories
                .Select(x =>
                    new DonorMedicalHistoryViewDto
                    {
                        Answer = x.Answer,
                        HeaderText = x.MedicalQuestionnaire.HeaderText,
                        MedicalQuestionnaireId = x.MedicalQuestionnaireId,
                        QuestionText = x.MedicalQuestionnaire.QuestionTagalogText,
                        Remarks = x.Remarks
                    })
                .ToList();

            return dto;
        }

        public async Task<string> RegisterDonor(RegisterDonorDto dto)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(DonorDto));

                if (dto.MedicalHistories == null) throw new ArgumentNullException(nameof(DonorMedicalHistory));


                var newDonorRegistration = mapper.Map<DonorRegistration>(dto);
                newDonorRegistration.RegistrationDate = DateTime.UtcNow;

                // generate registration number
                var guid = Guid.NewGuid();
                var rn = $"RN{guid.ToString().Substring(0, 8).ToUpper()}";
                newDonorRegistration.RegistrationNumber = rn;

                repository.DonorRegistration.Create(newDonorRegistration);

                var medicalHistories = mapper.Map<List<DonorMedicalHistory>>(dto.MedicalHistories);

                foreach (var item in medicalHistories)
                {
                    item.DonorRegistrationId = newDonorRegistration.DonorRegistrationId;
                }

                repository.DonorMedicalHistory.AddRange(medicalHistories);

                await repository.SaveAsync();

                return newDonorRegistration.RegistrationNumber;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VerifyDonorResultDto> VerifyDonor(VerifyDonorDto dto)
        {
            var result = new VerifyDonorResultDto() { IsValid = true };

            var registrant = await dbContext.DonorRegistrations
                .Include(x => x.DonorTransactions)
                .Where(x => x.Firstname.ToLower() == dto.Firstname.ToLower()
                    && x.Lastname.ToLower() == dto.Lastname.ToLower()
                    && x.Middlename.ToLower() == dto.Middlename.ToLower()
                    && x.BirthDate.Date == dto.BirthDate.Date)
                .OrderByDescending(x => x.RegistrationDate)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var donor = await dbContext.Donors
                .Where(x => x.Firstname.ToLower() == dto.Firstname.ToLower()
                    && x.Lastname.ToLower() == dto.Lastname.ToLower()
                    && x.Middlename.ToLower() == dto.Middlename.ToLower()
                    && x.BirthDate.Date == dto.BirthDate.Date)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            // 1st time registrant
            if (registrant == null)
            {
                return result;
            }
            else
            {
                var hasPassedThreshold = (DateTime.UtcNow.Date - registrant.RegistrationDate.Date).TotalDays >= 14;

                // Existing registrant but not existing donor
                if (donor == null)
                {
                    if (!hasPassedThreshold)
                    {
                        var registrantStatus = registrant.DonorTransactions.FirstOrDefault(x => x.DonorRegistrationId == registrant.DonorRegistrationId)?.DonorStatus;
                        result = GetVerificationResultForRegisteredDonor(registrantStatus, registrant.RegistrationNumber);
                    }

                    return result;
                }
                else 
                { 
                    // Donor has existing record, then check donor latest history
                    var donorLatestTransaction = await dbContext.DonorTransactions
                           .Where(x => x.DonorId == donor.DonorId)
                           .OrderByDescending(x => x.DateOfDonation)
                           .AsNoTracking()
                           .FirstOrDefaultAsync();

                    var donorDto = mapper.Map<DonorDto>(donor);

                    // If donor's previous transaction wasn't finished (did not proceed initial screening to donating after donor screening and transaction creation)
                    if (donorLatestTransaction.DateOfDonation.HasValue == false)
                    {
                        if (!hasPassedThreshold)
                        {
                            result = GetVerificationResultForRegisteredDonor(donorLatestTransaction.DonorStatus, registrant.RegistrationNumber);
                        }

                        return result;
                    }

                    if (donorLatestTransaction.DonorStatus != DonorStatus.Deferred)
                    {
                        result = await ValidateDonor(donorLatestTransaction.DonorTransactionId, donorLatestTransaction.DateOfDonation.Value, donorDto);
                        return result;
                    }

                    var donorDeferral = await dbContext.DonorDeferrals.FirstOrDefaultAsync(x => x.DonorTransactionId == donorLatestTransaction.DonorTransactionId);

                    if (donorDeferral.DeferralStatus == DeferralStatus.Permanent)
                    {
                        result = new VerifyDonorResultDto()
                        {
                            IsValid = false,
                            DeferralStatus = donorDeferral.DeferralStatus,
                            Remarks = donorDeferral.Remarks
                        };
                        return result;
                    }

                    // If Deferral Status is Temporary check for previous latest donor history donation date
                    result = await ValidateDonor(donorLatestTransaction.DonorTransactionId, donorLatestTransaction.DateOfDonation.Value, donorDto);
                }
            }

            return result;
        }

        public async Task<Guid> UpdateDonorAndCreateTransaction(UpdateDonorInfoDto dto, Guid userId, bool isOffline)
        {
            var model = await dbContext.DonorRegistrations
               .Include(x => x.DonorMedicalHistories)
               .FirstOrDefaultAsync(x => x.DonorRegistrationId == dto.DonorRegistrationId);

            if (model == null)
            {
                throw new RecordNotFoundException("DonorRegistration record not found.");
            }

            // Update donor registration for any changes has been made
            UpdateDonorRegistrationInfo(model, dto);

            // Check if registered donor already exists in the Donor table
            var donor = await dbContext.Donors.FirstOrDefaultAsync(x =>
                x.Firstname.ToLower() == dto.Firstname.ToLower() &&
                x.Lastname.ToLower() == dto.Lastname.ToLower() &&
                x.Middlename.ToLower() == dto.Middlename.ToLower() &&
                x.BirthDate.Date == dto.BirthDate.Date);

            if (donor != null)
            {
                UpdateExistingDonor(donor, dto, userId);
            }
            else
            {
                donor = mapper.Map<Donor>(dto);
                donor.UpdatedBy = userId;
                donor.UpdatedAt = DateTime.UtcNow;
                await dbContext.Donors.AddAsync(donor);
            }

            var newDonorTransaction = new DonorTransaction
            {
                DonorId = donor.DonorId,
                DonorRegistrationId = model.DonorRegistrationId,
                BloodIsSafeToTransfuse = true,
                //DOHNBBNetsBarcode = "",
                DonorStatus = DonorStatus.ForVitalSigns,
                IsOffline = isOffline,
                IsSync = !isOffline,
                SegmentSerialNumber = ""
            };

            await dbContext.DonorTransactions.AddAsync(newDonorTransaction);

            await dbContext.SaveChangesAsync();

            return newDonorTransaction.DonorTransactionId;
        }

        private async Task<VerifyDonorResultDto> ValidateDonor(Guid donorTransactionId, DateTime donationDate, DonorDto donor)
        {
            var result = new VerifyDonorResultDto() { IsValid = true };

            var currentDate = DateTime.UtcNow;
            var donorBloodCollection = await dbContext.DonorBloodCollections.FirstOrDefaultAsync(x => x.DonorTransactionId == donorTransactionId);

            // Apheresis blood re-collection should be 2 weeks or more
            if (donorBloodCollection != null && donorBloodCollection.CollectionType == BloodBagCollectionTypes.Apheresis)
            {
                var ts = currentDate.Subtract(donationDate);
                var dateDiff = ts.Days;
                var totalWeeks = (int)dateDiff / 7;

                result.IsValid = totalWeeks >= 2;

                if (result.IsValid)
                {
                    result.Donor = donor;
                }
                else
                {
                    result.DeferralStatus = DeferralStatus.Temporary;
                    result.Remarks = $"Apheresis, current week count: {totalWeeks}";
                }
            }
            else // For whole blood collection it should 3 months or more
            {
                int monthsApart = 12 * (donationDate.Year - currentDate.Year) + donationDate.Month - currentDate.Month;
                var pastMonths = Math.Abs(monthsApart);

                result.IsValid = pastMonths >= 3;

                if (result.IsValid)
                {
                    result.Donor = donor;
                }
                else
                {
                    result.DeferralStatus = DeferralStatus.Temporary;
                    result.Remarks = $"Current month count: {pastMonths}";
                }
            }

            return result;
        }

        private void UpdateDonorRegistrationInfo(DonorRegistration model, UpdateDonorInfoDto dto)
        {
            model.Firstname = dto.Firstname;
            model.Middlename = dto.Middlename;
            model.Lastname = dto.Lastname;
            model.BirthDate = dto.BirthDate;
            model.CivilStatus = dto.CivilStatus;
            model.Gender = dto.Gender;
            model.AddressNo = dto.AddressNo;
            model.AddressStreet = dto.AddressStreet;
            model.AddressBarangay = dto.AddressBarangay;
            model.AddressMunicipality = dto.AddressMunicipality;
            model.AddressProvinceOrCity = dto.AddressProvinceOrCity;
            model.AddressZipcode = dto.AddressZipcode;
            model.OfficeAddress = dto.OfficeAddress;
            model.Religion = dto.Religion;
            model.Nationality = dto.Nationality;
            model.Education = dto.Education;
            model.WorkName = dto.WorkName;
            model.TelNo = dto.TelNo;
            model.MobileNo = dto.MobileNo;
            model.EmailAddress = dto.EmailAddress;
            model.SchoolIdNo = dto.SchoolIdNo;
            model.CompanyIdNo = dto.CompanyIdNo;
            model.PRCNo = dto.PRCNo;
            model.DriverLicenseNo = dto.DriverLicenseNo;
            model.SssGsisBirNo = dto.SssGsisBirNo;
            model.OtherNo = dto.OtherNo;

            foreach (var item in model.DonorMedicalHistories)
            {
                var qq = dto.MedicalHistories.FirstOrDefault(x => x.MedicalQuestionnaireId == item.MedicalQuestionnaireId);

                if (qq != null)
                {
                    item.Answer = qq.Answer;
                    item.Remarks = qq.Remarks;
                }
            }
        }

        private void UpdateExistingDonor(Donor model, UpdateDonorInfoDto dto, Guid userId)
        {
            model.Firstname = dto.Firstname;
            model.Middlename = dto.Middlename;
            model.Lastname = dto.Lastname;
            model.BirthDate = dto.BirthDate;
            model.CivilStatus = dto.CivilStatus;
            model.Gender = dto.Gender;
            model.AddressNo = dto.AddressNo;
            model.AddressStreet = dto.AddressStreet;
            model.AddressBarangay = dto.AddressBarangay;
            model.AddressMunicipality = dto.AddressMunicipality;
            model.AddressProvinceOrCity = dto.AddressProvinceOrCity;
            model.AddressZipcode = dto.AddressZipcode;
            model.OfficeAddress = dto.OfficeAddress;
            model.Religion = dto.Religion;
            model.Nationality = dto.Nationality;
            model.Education = dto.Education;
            model.WorkName = dto.WorkName;
            model.TelNo = dto.TelNo;
            model.MobileNo = dto.MobileNo;
            model.EmailAddress = dto.EmailAddress;
            model.SchoolIdNo = dto.SchoolIdNo;
            model.CompanyIdNo = dto.CompanyIdNo;
            model.PRCNo = dto.PRCNo;
            model.DriverLicenseNo = dto.DriverLicenseNo;
            model.SssGsisBirNo = dto.SssGsisBirNo;
            model.OtherNo = dto.OtherNo;
            model.UpdatedAt = DateTime.UtcNow;
            model.UpdatedBy = userId;
        }
        
        private VerifyDonorResultDto GetVerificationResultForRegisteredDonor(string donorStatus, string registrationNumber)
        {
            return new VerifyDonorResultDto()
            {
                IsValid = false,
                DeferralStatus = donorStatus ?? "Pending Screening",
                Remarks = $"You have already registered. This is your reference number: {registrationNumber}. Please proceed to assessment window."
            };
        }
    }
}
