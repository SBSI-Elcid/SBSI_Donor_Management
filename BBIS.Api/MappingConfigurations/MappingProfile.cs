using AutoMapper;
using BBIS.Application.DTOs.ApplicationSetting;
using BBIS.Application.DTOs.DonorRegistration;
using BBIS.Application.DTOs.DonorScreening;
using BBIS.Application.DTOs.Schedule;
using BBIS.Application.DTOs.Patient;
using BBIS.Application.DTOs.Requisition;
using BBIS.Application.DTOs.Signatory;
using BBIS.Application.DTOs.UserAccount;
using BBIS.Domain.Models;

namespace BBIS.Api.MappingConfigurations
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserAccountDto, UserAccount>();
            CreateMap<UpdateUserAccountDto, UserAccount>();
            CreateMap<UserAccount, UserAccountViewDto>();
            CreateMap<MedicalQuestionnaire, MedicalQuestionnaireDto>();
            CreateMap<DonorInitialScreeningDto, DonorInitialScreening>().ReverseMap();
            CreateMap<DonorVitalSignsDto, DonorVitalSigns>().ReverseMap();
            CreateMap<DonorRecentDonationDto, DonorRecentDonation>().ReverseMap();
            CreateMap<DonorPhysicalExaminationDto, DonorPhysicalExamination>().ReverseMap();
            CreateMap<DonorBloodBagInfo, DonorBloodBagIssuance>().ReverseMap();
            CreateMap<DonorBloodBagIssuanceDto, DonorBloodBagIssuance>().ReverseMap();
            CreateMap<DonorBloodCollectionDto, DonorBloodCollection>().ReverseMap();
            CreateMap<DonorPostDonationCareDto, DonorPostDonationCare>().ReverseMap();
            CreateMap<VitalSignsMonitoringDto, VitalSignsMonitoring>().ReverseMap();
            CreateMap<PostDonationDetailsDto, PostDonationDetail>().ReverseMap();
            CreateMap<ScheduleDto, Schedule>().ReverseMap();
            CreateMap<ActivityDonorDto, ActivityDonor>().ReverseMap();
            CreateMap<ChecklistDto, checklist>().ReverseMap();
            CreateMap<DonorMedicalQuestionnaireDto, DonorMedicalHistory>();
            CreateMap<DonorDeferralDto, DonorDeferral>();
            CreateMap<RegisterDonorDto, DonorRegistration>();
            CreateMap<DonorRegistration, RegisteredDonorInfoDto>();
            CreateMap<UpdateDonorInfoDto, Donor>();
            CreateMap<DonorDto, Donor>().ReverseMap();
            CreateMap<ApplicationSettingDto, ApplicationSetting>().ReverseMap();
            CreateMap<BloodComponentSettingDto, BloodComponent>().ReverseMap();
            CreateMap<InstitutionDto, Institution>().ReverseMap();
            CreateMap<PatientDto, Patient>().ReverseMap();
            CreateMap<BloodComponentChecklistDto, BloodComponentChecklist>().ReverseMap();
            CreateMap<TestOrderTypeSettingDto, TestOrderType>().ReverseMap();
            CreateMap<ReservationDto, Reservation>().ReverseMap();
            CreateMap<Transfusion, TransfusionDto>().ReverseMap();
            CreateMap<TransfusionVitalSign, TransfusionVitalSignDto>().ReverseMap();
            CreateMap<SignatoryDto, Signatory>().ReverseMap();

            //Libraries
            CreateMap<RoleDto, Role>().ReverseMap();
        }
    }
}
