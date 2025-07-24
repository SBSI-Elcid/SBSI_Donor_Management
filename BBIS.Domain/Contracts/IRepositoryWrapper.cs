namespace BBIS.Domain.Contracts
{
    public interface IRepositoryWrapper
    {
        IUserAccountRepository UserAccount { get; }
        IDonorRepository Donor { get; }
        IDonorTransactionRepository DonorTransaction { get; }
        IDonorMedicalHistoryRepository DonorMedicalHistory { get; }
        IDonorInitialScreeningRepository DonorInitialScreening { get; }
        IDonorVitalSignsRepository DonorVitalSigns { get; }
        IDonorBloodBagIssuanceRepository DonorBloodBagIssuance { get; }
        IDonorPhysicalExaminationRepository DonorPhysicalExamination { get; }
        IDonorBloodCollectionRepository DonorBloodCollection { get; }
        IDonorPostDonationCareRepository DonorPostDonationCare { get; }
        IScheduleRepository Schedule { get; } 
        IChecklistRepository Checklist { get; }
        IActivityDonor ActivityDonor { get; }
        IPostDonationDetailsRepository PostDonationDetail { get; }
        IDonorVitalSignsMonitoring VitalSignsMonitoring { get; }
        IDonorRecentDonationRepository DonorRecentDonation { get; }
        IDonorDeferralRepository DonorDeferral { get; }
        IDonorRegistrationRepository DonorRegistration { get; }
        ILibrariesRole Role { get; }
        ILibraryQuestionnareRepository MedicalQuestionnare { get; }
        IApplicationSettingRepository ApplicationSetting { get; }
        IBloodComponentRespository BloodComponent { get; }
        IInstitutionRepository Institution { get; }
        IInventorySourceRepository InventorySource { get; }
        IInventoryItemRepository InventoryItem { get; }
        IPatientRepository Patient { get; }
        IBloodRequestRepository BloodRequest { get; }
        IBloodRequestTestOrderRepository BloodRequestTestOrder { get; }
        IBloodComponentChecklistRepository BloodComponentChecklist { get; }
        ITestOrderTypeRepository TestOrderType { get; }
        IOtherTestOrderRepository OtherTestOrder { get; }
        ICrossMatchTestOrderRepository CrossMatchTestOrder { get; }
        IReservationChecklistRepository ReservationChecklist { get; }
        IReservationRepository Reservation { get; }
        ITestOrderGroupRepository TestOrderGroup { get; }
        ITestOrderRepository TestOrder { get; }
        ITransfusionRepository Transfusion { get; }
        ISignatoryRepository Signatory { get; }

        IUserRoleScreeningAccessRepository UserRoleScreeningAccess { get; }

        void Save();

        Task SaveAsync();
    }
}
