using BBIS.Database.MigrationHelper;
using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBIS.Database
{
    public class BBDbContext: DbContext
    {
        public BBDbContext(DbContextOptions<BBDbContext> options) : base(options) { }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MedicalQuestionnaire> MedicalQuestionnaires { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<DonorRegistration> DonorRegistrations { get; set; }
        public DbSet<DonorTransaction> DonorTransactions { get; set; }
        public DbSet<DonorPostDonationCare> DonorPostDonationCares { get; set; }
        public DbSet<PostDonationDetail> PostDonationDetails { get; set; }
        public DbSet<VitalSignsMonitoring> VitalSignsMonitorings { get; set; }
        public DbSet<DonorRecentDonation> DonorRecentDonations { get; set; }
        public DbSet<DonorInitialScreening> DonorInitialScreenings { get; set; }
        public DbSet<DonorVitalSigns> DonorVitalSigns { get; set; }
        public DbSet<DonorMedicalHistory> DonorMedicalHistories { get; set; }
        public DbSet<DonorPhysicalExamination> DonorPhysicalExaminations { get; set; }
        public DbSet<DonorBloodBagIssuance> DonorBloodBagIssuances{ get; set; }
        public DbSet<DonorBloodCollection> DonorBloodCollections { get; set; }
        public DbSet<DonorDeferral> DonorDeferrals { get; set; }

        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<LookupOption> LookupOptions { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<BloodComponent> BloodComponents { get; set; }
        public DbSet<BloodTestType> BloodTestTypes { get; set; }
        public DbSet<DonorTestOrderTestType> DonorTestOrderTestTypes { get; set; }
        public DbSet<DonorTestOrder> DonorTestOrders { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InventorySource> InventorySources { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        // Requisitions
        public DbSet<BloodComponentChecklist> BloodComponentChecklists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationItem> ReservationItems { get; set; }
        public DbSet<ReservationChecklist> ReservationChecklists { get; set; }
        public DbSet<TestOrderType> TestOrderTypes { get; set; }
        public DbSet<TestOrder> TestOrders { get; set; }
        public DbSet<Transfusion> Transfusions { get; set; }
        public DbSet<TransfusionVitalSign> TransfusionVitalSigns { get; set; }
        public DbSet<CrossMatchTestOrder> CrossMatchTestOrders { get; set; }
        public DbSet<BloodScreeningTestOrder> BloodScreeningTestOrders { get; set; }
        public DbSet<CoombsTestOrder> CoombsTestOrders { get; set; }
        public DbSet<BloodTypingTestOrder> BloodTypingTestOrders { get; set; }
        public DbSet<Signatory> Signatories { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserModule> UserModules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(nameof(Role));
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleName).HasColumnType("VARCHAR(50)");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable(nameof(UserAccount));
                entity.HasKey(e => e.UserAccountId);
                entity.Property(e => e.Username).IsRequired().HasColumnType("varchar(45)");
                entity.Property(e => e.Firstname).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Lastname).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.EmailAddress).HasColumnType("varchar(150)");
                entity.Property(e => e.Password).HasColumnType("varchar(250)");
                entity.Property(e => e.PasswordSalt).HasColumnType("varchar(200)");

                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.Property(e => e.UpdatedBy).IsRequired();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable(nameof(UserRole));
                entity.HasKey(e => e.UserRoleId);

                entity.HasOne(d => d.UserAccount).WithMany(p => p.UserRoles).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MedicalQuestionnaire>(entity =>
            {
                entity.ToTable(nameof(MedicalQuestionnaire));
                entity.HasKey(e => e.MedicalQuestionnaireId);
                entity.Property(e => e.QuestionTagalogText).IsRequired().HasColumnType("varchar(500)");
                entity.Property(e => e.QuestionEnglishText).HasColumnType("varchar(500)");
                entity.Property(e => e.QuestionBisayaText).HasColumnType("varchar(500)");
                entity.Property(e => e.HeaderText).HasColumnType("varchar(200)");
                entity.Property(e => e.GenderOption).HasColumnType("varchar(10)");

                entity.HasMany(x => x.DonorMedicalHistories).WithOne(x => x.MedicalQuestionnaire)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Donor>(entity =>
            {
                entity.ToTable(nameof(Donor));
                entity.HasKey(e => e.DonorId);
                entity.Property(e => e.Middlename).IsRequired().HasColumnType("varchar(90)");
                entity.Property(e => e.Firstname).IsRequired().HasColumnType("varchar(90)");
                entity.Property(e => e.Lastname).IsRequired().HasColumnType("varchar(90)");
                entity.Property(e => e.CivilStatus).IsRequired().HasColumnType("varchar(20)");
                entity.Property(e => e.Gender).IsRequired().HasColumnType("varchar(10)");

                entity.Property(e => e.AddressNo).HasColumnType("varchar(20)");
                entity.Property(e => e.AddressStreet).HasColumnType("varchar(50)");
                entity.Property(e => e.AddressBarangay).IsRequired().HasColumnType("varchar(60)");
                entity.Property(e => e.AddressMunicipality).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.AddressProvinceOrCity).IsRequired().HasColumnType("varchar(70)");
                entity.Property(e => e.AddressZipcode).IsRequired().HasColumnType("varchar(10)");

                entity.Property(e => e.OfficeAddress).HasColumnType("varchar(250)");
                entity.Property(e => e.Religion).HasColumnType("varchar(60)");
                entity.Property(e => e.Nationality).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Education).HasColumnType("varchar(70)");
                entity.Property(e => e.WorkName).HasColumnType("varchar(90)");

                entity.Property(e => e.TelNo).HasColumnType("varchar(30)");
                entity.Property(e => e.MobileNo).HasColumnType("varchar(20)");
                entity.Property(e => e.EmailAddress).HasColumnType("varchar(60)");

                entity.Property(e => e.SchoolIdNo).HasColumnType("varchar(20)");
                entity.Property(e => e.CompanyIdNo).HasColumnType("varchar(20)");
                entity.Property(e => e.PRCNo).HasColumnType("varchar(30)");
                entity.Property(e => e.DriverLicenseNo).HasColumnType("varchar(30)");
                entity.Property(e => e.SssGsisBirNo).HasColumnType("varchar(40)");
                entity.Property(e => e.OtherNo).HasColumnType("varchar(20)");
                               
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.Property(e => e.UpdatedBy).IsRequired();

            });

            modelBuilder.Entity<DonorRegistration>(entity =>
            {
                entity.ToTable(nameof(DonorRegistration));
                entity.HasKey(e => e.DonorRegistrationId);
                entity.Property(e => e.Middlename).IsRequired().HasColumnType("varchar(90)");
                entity.Property(e => e.Firstname).IsRequired().HasColumnType("varchar(90)");
                entity.Property(e => e.Lastname).IsRequired().HasColumnType("varchar(90)");
                entity.Property(e => e.CivilStatus).IsRequired().HasColumnType("varchar(20)");
                entity.Property(e => e.Gender).IsRequired().HasColumnType("varchar(10)");

                entity.Property(e => e.AddressNo).HasColumnType("varchar(20)");
                entity.Property(e => e.AddressStreet).HasColumnType("varchar(50)");
                entity.Property(e => e.AddressBarangay).IsRequired().HasColumnType("varchar(60)");
                entity.Property(e => e.AddressMunicipality).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.AddressProvinceOrCity).IsRequired().HasColumnType("varchar(70)");
                entity.Property(e => e.AddressZipcode).IsRequired().HasColumnType("varchar(10)");

                entity.Property(e => e.OfficeAddress).HasColumnType("varchar(250)");
                entity.Property(e => e.Religion).HasColumnType("varchar(60)");
                entity.Property(e => e.Nationality).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Education).HasColumnType("varchar(70)");
                entity.Property(e => e.WorkName).HasColumnType("varchar(90)");

                entity.Property(e => e.TelNo).HasColumnType("varchar(30)");
                entity.Property(e => e.MobileNo).HasColumnType("varchar(20)");
                entity.Property(e => e.EmailAddress).HasColumnType("varchar(60)");

                entity.Property(e => e.SchoolIdNo).HasColumnType("varchar(20)");
                entity.Property(e => e.CompanyIdNo).HasColumnType("varchar(20)");
                entity.Property(e => e.PRCNo).HasColumnType("varchar(30)");
                entity.Property(e => e.DriverLicenseNo).HasColumnType("varchar(30)");
                entity.Property(e => e.SssGsisBirNo).HasColumnType("varchar(40)");
                entity.Property(e => e.OtherNo).HasColumnType("varchar(20)");

                entity.Property(e => e.RegistrationDate).IsRequired();
               
            });

            modelBuilder.Entity<DonorRecentDonation>(entity =>
            {
                entity.ToTable(nameof(DonorRecentDonation));
                entity.HasKey(e => e.DonorRecentDonationId);
                entity.Property(e => e.PlaceOfRecentDonation).HasColumnType("varchar(90)");
                entity.Property(e => e.Agency).HasColumnType("varchar(30)");

                entity.HasOne(x => x.Donor).WithMany(m => m.DonorRecentDonations)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DonorDeferral>(entity =>
            {
                entity.ToTable(nameof(DonorDeferral));
                entity.HasKey(e => e.DonorDeferralId);
                entity.Property(e => e.DeferralStatus).HasColumnType("varchar(15)");
                entity.Property(e => e.Remarks).HasColumnType("varchar(100)");

                entity.HasOne(x => x.DonorTransaction).WithOne(x => x.DonorDeferral).OnDelete(DeleteBehavior.NoAction);
            });
                      
            modelBuilder.Entity<DonorTransaction>(entity =>
            {
                entity.ToTable(nameof(DonorTransaction));
                entity.HasKey(e => e.DonorTransactionId);
                //entity.Property(e => e.PRCBloodDonorNumber).HasColumnType("varchar(30)");
                //entity.Property(e => e.DOHNBBNetsBarcode).HasColumnType("varchar(30)");
                //entity.Property(e => e.UnitSerialNumber).HasColumnType("varchar(30)");
                entity.Property(e => e.DonorStatus).HasColumnType("varchar(25)");
                //entity.Property(e => e.FinalBloodType).HasColumnType("varchar(4)");
                entity.Property(e => e.BloodRh).HasColumnType("varchar(15)");
                               
                entity.HasOne(x => x.Donor).WithMany(m => m.DonorTransactions).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.DonorRegistration).WithMany(m => m.DonorTransactions).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DonorInitialScreening>(entity =>
            {
                entity.ToTable(nameof(DonorInitialScreening));
                entity.HasKey(e => e.DonorInitialScreeningId);
                //entity.Property(e => e.BloodType).HasColumnType("varchar(10)");
                //entity.Property(e => e.DonationType).HasColumnType("varchar(25)");
                //entity.Property(e => e.InHouseTypeValue).HasColumnType("varchar(20)");
                //entity.Property(e => e.MobileBloodDonationPlace).HasColumnType("varchar(100)");
                //entity.Property(e => e.MobileBloodDonationOrganizer).HasColumnType("varchar(50)");
                //entity.Property(e => e.NameOfPatient).HasColumnType("varchar(100)");
                //entity.Property(e => e.PatientHospital).HasColumnType("varchar(100)");
                //entity.Property(e => e.PatientBloodType).HasColumnType("varchar(5)");
                //entity.Property(e => e.PatientWBOrComponent).HasColumnType("varchar(10)");
                //entity.Property(e => e.PatientNoOfUnits).HasColumnType("varchar(10)");
                //entity.Property(e => e.PrcOffice).HasColumnType("varchar(70)");

                entity.HasOne(x => x.DonorTransaction).WithOne(x => x.DonorInitialScreening).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DonorBloodBagIssuance>(entity =>
            {
                entity.ToTable(nameof(DonorBloodBagIssuance));
                entity.HasKey(e => e.DonorBloodBagIssuanceId);

                entity.HasOne(e => e.DonorTransaction)
                      .WithMany(e=>e.DonorBloodBagIssuances)
                      .HasForeignKey(e => e.DonorTransactionId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DonorPostDonationCare>(entity => {
                entity.ToTable(nameof(DonorPostDonationCare));
                entity.HasKey(e => e.DonorPostDonationCareId);

                entity.HasOne(e => e.DonorTransaction)
                      .WithOne(x => x.DonorPostDonationCare).OnDelete(DeleteBehavior.NoAction); 
            
            
            });


            modelBuilder.Entity<VitalSignsMonitoring>(entity => {
                entity.ToTable(nameof(VitalSignsMonitoring));
                entity.HasKey(e => e.VitalSignsMonitoringId);

                entity.HasOne(e => e.DonorPostDonationCare)
                      .WithMany(m => m.VitalSignsMonitorings).OnDelete(DeleteBehavior.NoAction);


            });

            modelBuilder.Entity<PostDonationDetail>(entity => {
                entity.ToTable(nameof(PostDonationDetail));
                entity.HasKey(e => e.PostDonationDetailsId);

                entity.HasOne(e => e.DonorPostDonationCare)
                      .WithMany(m => m.PostDonationDetails).OnDelete(DeleteBehavior.NoAction);


            });

            modelBuilder.Entity<DonorPhysicalExamination>(entity =>
            {
                entity.ToTable(nameof(DonorPhysicalExamination));
                entity.HasKey(e => e.DonorPhysicalExaminationId);
                //entity.Property(e => e.BloodPressure).HasColumnType("varchar(10)");
                entity.Property(e => e.GeneralStatus).HasColumnType("varchar(50)");
                entity.Property(e => e.Skin).HasColumnType("varchar(50)");
                entity.Property(e => e.HEENT).HasColumnType("varchar(50)");
                entity.Property(e => e.HeartAndLungs).HasColumnType("varchar(50)");
               // entity.Property(e => e.ResultStatus).HasColumnType("varchar(20)");
                //entity.Property(e => e.FailedRemarks).HasColumnType("varchar(200)");
                //entity.Property(e => e.BloodBagType).HasColumnType("varchar(15)");
                //entity.Property(e => e.DoctorName).HasColumnType("varchar(60)");

                entity.HasOne(x => x.DonorTransaction).WithOne(x => x.DonorPhysicalExamination).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DonorBloodCollection>(entity =>
            {
                entity.ToTable(nameof(DonorBloodCollection));
                entity.HasKey(e => e.DonorBloodCollectionId);
                //entity.Property(e => e.CollectionType).HasColumnType("varchar(15)");
                //entity.Property(e => e.CollectionSubType).HasColumnType("varchar(15)");
                entity.Property(e => e.UnitOfMeasurement).HasColumnType("varchar(10)");
                entity.Property(e => e.UnwellReason).HasColumnType("varchar(100)");
                //entity.Property(e => e.MedicationGiven).HasColumnType("varchar(100)");

                entity.HasOne(x => x.DonorTransaction).WithOne(x => x.DonorBloodCollection).OnDelete(DeleteBehavior.NoAction);
               
                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.DonorBloodCollections)
                    .HasForeignKey(d => d.FacilitatedById)
                    .HasConstraintName("FK_DonorBloodCollection_FacilitatedById");
            });

            modelBuilder.Entity<DonorMedicalHistory>(entity =>
            {
                entity.ToTable(nameof(DonorMedicalHistory));
                entity.HasKey(e => e.DonorMedicalHistoryId);
                entity.Property(e => e.Answer).HasColumnType("varchar(10)");
                entity.Property(e => e.Remarks).HasColumnType("varchar(100)");

                entity.HasOne(x => x.DonorRegistration).WithMany(x => x.DonorMedicalHistories).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Lookup>(entity =>
            {
                entity.ToTable(nameof(Lookup));
                entity.HasKey(e => e.LookupId);
                entity.Property(e => e.LookupKey).HasColumnType("varchar(30)");
                entity.Property(e => e.Description).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<LookupOption>(entity =>
            {
                entity.ToTable(nameof(LookupOption));
                entity.HasKey(e => e.LookupOptionId);
                entity.Property(e => e.Name).HasColumnType("varchar(50)");
                entity.Property(e => e.Value).HasColumnType("varchar(50)");

                entity.HasOne(x => x.Lookup).WithMany(x => x.LookupOptions).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.ToTable(nameof(ApplicationSetting));
                entity.HasKey(e => e.ApplicationSettingId);
                entity.Property(e => e.SettingKey).HasColumnType("varchar(100)");
                entity.Property(e => e.SettingValue).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<BloodComponent>(entity =>
            {
                entity.ToTable(nameof(BloodComponent));
                entity.HasKey(e => e.BloodComponentId);
                entity.Property(e => e.ComponentName).HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<BloodTestType>(entity =>
            {
                entity.ToTable(nameof(BloodTestType));
                entity.HasKey(e => e.BloodTestTypeId);
                entity.Property(e => e.TypeName).HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<DonorTestOrderTestType>(entity =>
            {
                entity.ToTable(nameof(DonorTestOrderTestType));
                entity.HasKey(e => e.DonorTestOrderTestTypeId);
                entity.Property(e => e.Remarks).HasColumnType("varchar(75)");

                entity.HasOne(x => x.DonorTestOrder).WithMany(x => x.DonorTestOrderTestTypes).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DonorTestOrder>(entity =>
            {
                entity.ToTable(nameof(DonorTestOrder));
                entity.HasKey(e => e.DonorTestOrderId);
                entity.HasOne(x => x.DonorTransaction).WithOne(x => x.DonorTestOrder).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Institution>(entity =>
            {
                entity.ToTable(nameof(Institution));
                entity.HasKey(e => e.InstitutionId);
                entity.Property(e => e.Name).HasColumnType("varchar(100)");
                entity.Property(e => e.Address).HasColumnType("varchar(200)");
                entity.Property(e => e.ContactNo).HasColumnType("varchar(20)");
                entity.Property(e => e.EmailAddress).HasColumnType("varchar(80)");

                entity.HasMany(x => x.InventorySources).WithOne(x => x.Institution).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<InventorySource>(entity =>
            {
                entity.ToTable(nameof(InventorySource));
                entity.HasKey(e => e.InventorySourceId);
                                
                entity.HasOne(d => d.DonorTransaction)
                    .WithMany(p => p.InventorySources)
                    .HasForeignKey(d => d.DonorTranctionId)
                    .HasConstraintName("FK_InventorySource_DonorTranctionId");
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.ToTable(nameof(InventoryItem));
                entity.HasKey(e => e.InventoryItemId);
                entity.Property(e => e.BloodType).HasColumnType("varchar(5)");
                entity.Property(e => e.UnitSerialNumber).HasColumnType("varchar(30)");
                entity.Property(e => e.UnitMeasure).HasColumnType("varchar(5)");
                entity.Property(e => e.Status).HasColumnType("varchar(15)");
                entity.Property(e => e.BloodRh).HasColumnType("varchar(15)");

                entity.HasOne(x => x.InventorySource).WithMany(x => x.InventoryItems).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.BloodComponent).WithMany(x => x.InventoryItems).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable(nameof(Patient));
                entity.HasKey(e => e.PatientId);
                entity.Property(e => e.BloodType).HasColumnType("varchar(4)");
                entity.Property(e => e.Rh).HasColumnType("varchar(13)");
                entity.Property(e => e.PatientIdNo).HasColumnType("varchar(15)");
                entity.Property(e => e.LastName).HasColumnType("varchar(50)");
                entity.Property(e => e.FirstName).HasColumnType("varchar(50)");
                entity.Property(e => e.MiddleName).HasColumnType("varchar(50)");
                entity.Property(e => e.Gender).HasColumnType("varchar(7)");
                entity.Property(e => e.CivilStatus).HasColumnType("varchar(15)");
                entity.Property(e => e.ContactNumber).HasColumnType("varchar(25)");
                entity.Property(e => e.Address).HasColumnType("varchar(255)");

                entity.HasMany(x => x.Reservations).WithOne(x => x.Patient).OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(x => x.TestOrders).WithOne(x => x.Patient).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable(nameof(Reservation));
                entity.HasKey(e => e.ReservationId);
                entity.Property(e => e.PatientType).HasColumnType("varchar(15)");
                entity.Property(e => e.PriorityLevel).HasColumnType("varchar(15)");
                entity.Property(e => e.RequestedBy).HasColumnType("varchar(55)");
                entity.Property(e => e.RoomNo).HasColumnType("varchar(10)");
                entity.Property(e => e.Membership).HasColumnType("varchar(10)");
                entity.Property(e => e.Status).HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<ReservationItem>(entity =>
            {
                entity.ToTable(nameof(ReservationItem));
                entity.HasKey(e => e.ReservationItemId);
                entity.Property(e => e.OtherNotes).HasColumnType("varchar(200)");
                entity.Property(e => e.DonorUnitSerialNumber).HasColumnType("varchar(30)");

                entity.HasOne(x => x.Reservation).WithMany(x => x.ReservationItems).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.InventoryItem).WithMany(x => x.ReservationItems).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.BloodComponent).WithMany(x => x.ReservationItems).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.DonorTransaction).WithMany(x => x.ReservationItems).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BloodComponentChecklist>(entity =>
            {
                entity.ToTable(nameof(BloodComponentChecklist));
                entity.HasKey(e => e.BloodComponentChecklistId);
                entity.Property(e => e.ChecklistDescription).HasColumnType("varchar(500)");

                entity.HasOne(x => x.BloodComponent).WithMany(x => x.BloodComponentChecklists).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ReservationChecklist>(entity =>
            {
                entity.ToTable(nameof(ReservationChecklist));
                entity.HasKey(e => e.ReservationChecklistId);
                                             
                entity.HasOne(x => x.BloodComponentChecklist).WithMany(x => x.ReservationChecklists).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.ReservationItem).WithMany(x => x.ReservationChecklists).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BloodScreeningTestOrder>(entity =>
            {
                entity.ToTable(nameof(BloodScreeningTestOrder));
                entity.HasKey(e => e.BloodScreeningTestOrderId);
                entity.Property(e => e.Result).HasColumnType("varchar(15)");
                entity.HasOne(x => x.TestOrder).WithOne(x => x.BloodScreeningTestOrder).OnDelete(DeleteBehavior.NoAction);
            });
                        
            modelBuilder.Entity<BloodTypingTestOrder>(entity =>
            {
                entity.ToTable(nameof(BloodTypingTestOrder));
                entity.HasKey(e => e.BloodTypingTestOrderId);
                entity.Property(e => e.BloodType).HasColumnType("varchar(4)");
                entity.Property(e => e.BloodRh).HasColumnType("varchar(15)");
                entity.Property(e => e.FTAntiA).HasColumnType("varchar(10)");
                entity.Property(e => e.FTAntiB).HasColumnType("varchar(10)");
                entity.Property(e => e.FTAntiAB).HasColumnType("varchar(10)");
                entity.Property(e => e.FTAntiD).HasColumnType("varchar(10)");
                entity.Property(e => e.FTAntiD2).HasColumnType("varchar(10)");

                entity.Property(e => e.RTACells).HasColumnType("varchar(10)");
                entity.Property(e => e.RTBCells).HasColumnType("varchar(10)");

                entity.Property(e => e.Control).HasColumnType("varchar(10)");
                entity.Property(e => e.FTAntiA).HasColumnType("varchar(10)");

                entity.HasOne(x => x.TestOrder).WithOne(x => x.BloodTypingTestOrder).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CoombsTestOrder>(entity =>
            {
                entity.ToTable(nameof(CoombsTestOrder));
                entity.HasKey(e => e.CoombsTestOrderId);

                entity.Property(e => e.IATResult).HasColumnType("varchar(15)");
                entity.Property(e => e.DATResult).HasColumnType("varchar(15)");

                entity.HasOne(x => x.TestOrder).WithOne(x => x.CoombsTestOrder).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CrossMatchTestOrder>(entity =>
            {
                entity.ToTable(nameof(CrossMatchTestOrder));
                entity.HasKey(e => e.CrossMatchTestOrderId);

                entity.Property(e => e.CrossMatchType).HasColumnType("varchar(20)");
                entity.Property(e => e.Result).HasColumnType("varchar(15)");
                entity.Property(e => e.Source).HasColumnType("varchar(50)");
                entity.Property(e => e.LISS_AGH).HasColumnType("varchar(5)");
                entity.Property(e => e.DonorUnitSerialNumber).HasColumnType("varchar(30)");

                entity.HasOne(x => x.TestOrder).WithMany(x => x.CrossMatchTestOrders).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.DonorTransaction).WithMany(x => x.CrossMatchTestOrders).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.BloodComponent).WithMany(x => x.CrossMatchTestOrders).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TestOrder>(entity =>
            {
                entity.ToTable(nameof(TestOrder));
                entity.HasKey(e => e.TestOrderId);
                entity.Property(e => e.TestOrderNumber).HasColumnType("varchar(15)");
                entity.Property(e => e.Remarks).HasColumnType("varchar(250)");

                entity.HasOne(x => x.Reservation).WithMany(x => x.TestOrders).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TestOrderType>(entity =>
            {
                entity.ToTable(nameof(TestOrderType));
                entity.HasKey(e => e.TestOrderTypeId);
                entity.Property(e => e.Code).HasColumnType("varchar(10)");
                entity.Property(e => e.Name).HasColumnType("varchar(100)");
                entity.Property(e => e.Description).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Transfusion>(entity =>
            {
                entity.ToTable(nameof(Transfusion));
                entity.HasKey(e => e.TransfusionId);
                entity.Property(e => e.MedicationGiven).HasColumnType("varchar(100)");
                entity.Property(e => e.HookedBy).HasColumnType("varchar(100)");
                entity.Property(e => e.RemovedBy).HasColumnType("varchar(100)");
                entity.Property(e => e.TransfusionStatus).HasColumnType("varchar(15)");
                entity.Property(e => e.TransfusionNotes).HasColumnType("varchar(250)");

                entity.HasOne(x => x.ReservationItem).WithOne(x => x.Transfusion).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Reservation).WithMany(x => x.Transfusions).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TransfusionVitalSign>(entity =>
            {
                entity.ToTable(nameof(TransfusionVitalSign));
                entity.HasKey(e => e.VitalSignId);
                entity.Property(e => e.VitalSignType).HasColumnType("varchar(20)");
                entity.Property(e => e.BloodPressure).HasColumnType("varchar(10)");
                entity.Property(e => e.Remarks).HasColumnType("varchar(250)");

                entity.HasOne(x => x.Transfusion).WithMany(x => x.TransfusionVitalSigns).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Signatory>(entity =>
            {
                entity.ToTable(nameof(Signatory));
                entity.HasKey(e => e.SignatoryId);
                entity.Property(e => e.FirstName).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.MiddleName).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.LastName).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Position).IsRequired().HasColumnType("varchar(100)");
                entity.Property(e => e.PositionPrefix).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.LicenseNo).IsRequired().HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.ToTable(nameof(UserRefreshToken));
                entity.HasKey(e => e.UserRefreshTokenId);
                entity.Property(e => e.RefreshToken).IsRequired().HasColumnType("varchar(250)");

                entity.HasOne(x => x.UserAccount).WithOne(x => x.UserRefreshToken).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable(nameof(Module));
                entity.HasKey(e => e.ModuleId);
                entity.Property(e => e.Menu).HasColumnType("VARCHAR(60)");
                entity.Property(e => e.Icon).HasColumnType("VARCHAR(50)");
                entity.Property(e => e.Link).HasColumnType("VARCHAR(90)");
            });

            modelBuilder.Entity<UserModule>(entity =>
            {
                entity.ToTable(nameof(UserModule));
                entity.HasKey(e => e.UserModuleId);

                entity.HasOne(d => d.UserAccount).WithMany(p => p.UserModules).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(d => d.Module).WithMany(p => p.UserModules).OnDelete(DeleteBehavior.NoAction);
            });

            // Seeding initial data 
            DataSeedFactory.SeedData(modelBuilder);
        }
    }
}
