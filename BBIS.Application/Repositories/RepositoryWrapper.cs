using BBIS.Database;
using BBIS.Domain.Contracts;

namespace BBIS.Application.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private BBDbContext _dbContext;
        private IUserAccountRepository _userAccount;
        private IDonorRepository _donor;
        private IDonorTransactionRepository _donorTransaction;
        private IDonorMedicalHistoryRepository _donorMedicalHistory;
        private IDonorInitialScreeningRepository _donorInitialScreening;
        private IDonorVitalSignsRepository _donorVitalSigns;
        private IDonorBloodBagIssuanceRepository _donorBloodBagIssuance;
        private IDonorPhysicalExaminationRepository _donorPhysicalExamination;
        private IDonorRecentDonationRepository _donorRecentDonation;
        private IDonorBloodCollectionRepository _donorBloodCollection;
        private IDonorPostDonationCareRepository _donorPostDonationCare;
        private IDonorVitalSignsMonitoring _vitalSignsMonitoring ;
        private IDonorDeferralRepository _donorDeferral;
        private IDonorRegistrationRepository _donorRegistration;
        private IApplicationSettingRepository _applicationSetting;
        private IBloodComponentRespository _bloodComponent;
        private IInstitutionRepository _institution;
        private IInventorySourceRepository _inventorySource;
        private IInventoryItemRepository _inventoryItem;
        private IPatientRepository _patient;
        private IBloodRequestRepository _bloodRequest;
        private IBloodRequestTestOrderRepository _bloodRequestTestOrder;
        private IBloodComponentChecklistRepository _bloodComponentChecklist;
        private ITestOrderTypeRepository _testOrderType;
        private IOtherTestOrderRepository _otherTestOrder;
        private ICrossMatchTestOrderRepository _crossMatchTestOrder;
        private IReservationChecklistRepository _reservationChecklist;
        private IReservationRepository _reservation;
        private ITestOrderGroupRepository _testOrderGroup;
        private ITestOrderRepository _testOrder;
        private ITransfusionRepository _transfusion;
        private ISignatoryRepository _signatory;

        public RepositoryWrapper(BBDbContext bloodbankDbContext)
        {
            _dbContext = bloodbankDbContext;
        }

        public IUserAccountRepository UserAccount
        {
            get
            {
                if (_userAccount == null)
                {
                    _userAccount = new UserAccountRepository(_dbContext);
                }
                return _userAccount;
            }
        }

        public IDonorRepository Donor
        {
            get
            {
                if (_donor == null)
                {
                    _donor = new DonorRepository(_dbContext);
                }
                return _donor;
            }
        }

        public IDonorTransactionRepository DonorTransaction
        {
            get
            {
                if (_donorTransaction == null)
                {
                    _donorTransaction = new DonorTransactionRepository(_dbContext);
                }
                return _donorTransaction;
            }
        }

        public IDonorMedicalHistoryRepository DonorMedicalHistory
        {
            get
            {
                if (_donorMedicalHistory == null)
                {
                    _donorMedicalHistory = new DonorMedicalHistoryRepository(_dbContext);
                }
                return _donorMedicalHistory;
            }
        }

        public IDonorInitialScreeningRepository DonorInitialScreening
        {
            get
            {
                if (_donorInitialScreening == null)
                {
                    _donorInitialScreening = new DonorInitialScreeningRepository(_dbContext);
                }
                return _donorInitialScreening;
            }
        }

        public IDonorVitalSignsRepository DonorVitalSigns
        {
            get
            {
                if (_donorVitalSigns == null)
                {
                    _donorVitalSigns = new DonorVitalSignsRepository(_dbContext);
                }
                return _donorVitalSigns;
            }
        }

        public IDonorBloodBagIssuanceRepository DonorBloodBagIssuance
        {
            get
            {
                if (_donorBloodBagIssuance == null)
                {
                    _donorBloodBagIssuance = new DonorBloodBagIssuanceRepository(_dbContext);
                }
                return _donorBloodBagIssuance;
            }
        }

        public IDonorPostDonationCareRepository DonorPostDonationCare
        {
            get
            {
                if (_donorPostDonationCare == null)
                {
                    _donorPostDonationCare = new DonorPostDonationCareRepository(_dbContext);
                }
                return _donorPostDonationCare;
            }
        }

        public IDonorVitalSignsMonitoring VitalSignsMonitoring
        {
            get
            {
                if (_vitalSignsMonitoring == null)
                {
                    _vitalSignsMonitoring = new VitalSignsMonitoringRepository(_dbContext);
                }
                return _vitalSignsMonitoring;
            }
        }

        public IDonorPhysicalExaminationRepository DonorPhysicalExamination
        {
            get
            {
                if (_donorPhysicalExamination == null)
                {
                    _donorPhysicalExamination = new DonorPhysicalExaminationRepository(_dbContext);
                }
                return _donorPhysicalExamination;
            }
        }

        public IDonorBloodCollectionRepository DonorBloodCollection
        {
            get
            {
                if (_donorBloodCollection == null)
                {
                    _donorBloodCollection = new DonorBloodCollectionRepository(_dbContext);
                }
                return _donorBloodCollection;
            }
        }

        public IDonorRecentDonationRepository DonorRecentDonation
        {
            get
            {
                if (_donorRecentDonation == null)
                {
                    _donorRecentDonation = new DonorRecentDonationRepository(_dbContext);
                }
                return _donorRecentDonation;
            }
        }

        public IDonorDeferralRepository DonorDeferral
        {
            get
            {
                if (_donorDeferral == null)
                {
                    _donorDeferral = new DonorDeferralRepository(_dbContext);
                }
                return _donorDeferral;
            }
        }

        public IDonorRegistrationRepository DonorRegistration
        {
            get
            {
                if (_donorRegistration == null)
                {
                    _donorRegistration = new DonorRegistrationRepository(_dbContext);
                }
                return _donorRegistration;
            }
        }

        public IApplicationSettingRepository ApplicationSetting
        {
            get
            {
                if (_applicationSetting == null)
                {
                    _applicationSetting = new ApplicationSettingRepository(_dbContext);
                }
                return _applicationSetting;
            }
        }

        public IBloodComponentRespository BloodComponent
        {
            get
            {
                if (_bloodComponent == null)
                {
                    _bloodComponent = new BloodComponentRepository(_dbContext);
                }
                return _bloodComponent;
            }
        }

        public IInventorySourceRepository InventorySource
        {
            get
            {
                if (_inventorySource == null)
                {
                    _inventorySource = new InventorySourceRepository(_dbContext);
                }
                return _inventorySource;
            }
        }

        public IInventoryItemRepository InventoryItem
        {
            get
            {
                if (_inventoryItem == null)
                {
                    _inventoryItem = new InventoryItemRepository(_dbContext);
                }
                return _inventoryItem;
            }
        }

        public IInstitutionRepository Institution
        {
            get
            {
                if (_institution == null)
                {
                    _institution = new InstitutionRepository(_dbContext);
                }
                return _institution;
            }
        }

        public IPatientRepository Patient 
        {
            get
            {
                if (_patient == null)
                {
                    _patient = new PatientRepository(_dbContext);
                }
                return _patient;
            }
        }

        public IBloodRequestRepository BloodRequest
        {
            get
            {
                if (_bloodRequest == null)
                {
                    _bloodRequest = new BloodRequestRepository(_dbContext);
                }
                return _bloodRequest;
            }
        }

        public IBloodRequestTestOrderRepository BloodRequestTestOrder
        {
            get
            {
                if (_bloodRequestTestOrder == null)
                {
                    _bloodRequestTestOrder = new BloodRequestTestOrderRepository(_dbContext);
                }
                return _bloodRequestTestOrder;
            }
        }

        public IBloodComponentChecklistRepository BloodComponentChecklist
        {
            get
            {
                if (_bloodComponentChecklist == null)
                {
                    _bloodComponentChecklist = new BloodComponentChecklistRepository(_dbContext);
                }
                return _bloodComponentChecklist;
            }
        }

        public ITestOrderTypeRepository TestOrderType
        {
            get
            {
                if (_testOrderType == null)
                {
                    _testOrderType = new TestOrderTypeRepository(_dbContext);
                }
                return _testOrderType;
            }
        }

        public IOtherTestOrderRepository OtherTestOrder
        {
            get
            {
                if (_otherTestOrder == null)
                {
                    _otherTestOrder = new OtherTestOrderRepository(_dbContext);
                }
                return _otherTestOrder;
            }
        }

        public ICrossMatchTestOrderRepository CrossMatchTestOrder
        {
            get
            {
                if (_crossMatchTestOrder == null)
                {
                    _crossMatchTestOrder = new CrossMatchTestOrderRepository(_dbContext);
                }
                return _crossMatchTestOrder;
            }
        }

        public IReservationChecklistRepository ReservationChecklist
        {
            get
            {
                if (_reservationChecklist == null)
                {
                    _reservationChecklist = new ReservationChecklistRepository(_dbContext);
                }
                return _reservationChecklist;
            }
        }

        public IReservationRepository Reservation
        {
            get
            {
                if (_reservation == null)
                {
                    _reservation = new ReservationRepository(_dbContext);
                }
                return _reservation;
            }
        }

        public ITestOrderGroupRepository TestOrderGroup
        {
            get
            {
                if (_testOrderGroup == null)
                {
                    _testOrderGroup = new TestOrderGroupRepository(_dbContext);
                }
                return _testOrderGroup;
            }
        }

        public ITestOrderRepository TestOrder
        {
            get
            {
                if (_testOrder == null)
                {
                    _testOrder = new TestOrderRepository(_dbContext);
                }
                return _testOrder;
            }
        }

        public ITransfusionRepository Transfusion
        {
            get
            {
                if (_transfusion == null)
                {
                    _transfusion = new TransfusionRepository(_dbContext);
                }
                return _transfusion;
            }
        }

        public ISignatoryRepository Signatory
        {
            get
            {
                if (_signatory == null)
                {
                    _signatory = new SignatoryRepository(_dbContext);
                }
                return _signatory;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
