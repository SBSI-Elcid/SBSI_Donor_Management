namespace BBIS.Common.Enums
{
    public static class QuestionnaireTypes
    {
        public static readonly string Yes = nameof(Yes);
        public static readonly string No = nameof(Yes);
    }

    public static class AgencyTypes
    {
        public static readonly string RedCross = nameof(RedCross);
        public static readonly string Hospital = nameof(Hospital);
    }

    public static class BloodBagTypes
    {
        public static readonly string Single = nameof(Single);
        public static readonly string Multiple = nameof(Multiple);
        public static readonly string TopAndBottom = nameof(TopAndBottom);
        public static readonly string Apheresis = nameof(Apheresis);
    }

    public static class BloodDonationTypes
    {
        public static readonly string InHouse = nameof(InHouse);
        public static readonly string MobileBloodDonation = nameof(MobileBloodDonation);
    }

    public static class BloodTypes
    {
        public static readonly string O = "O";
        public static readonly string A = "A";
        public static readonly string B = "B";
        public static readonly string AB = "AB";
    }

    public static class InHouseTypes
    {
        public static readonly string WalkInOrVolountary = nameof(WalkInOrVolountary);
        public static readonly string Replacement = nameof(Replacement);
        public static readonly string PatientDirected = nameof(PatientDirected);
    }

    public static class DeferralStatus
    {
        public static readonly string Permanent = nameof(Permanent);
        public static readonly string SelfDeferred = nameof(SelfDeferred);
        public static readonly string Indefinite = nameof(Indefinite);
        public static readonly string Temporary = nameof(Temporary);
    }

    public static class DonorStatus
    {
        public static readonly string ForInitialScreening = nameof(ForInitialScreening);
        public static readonly string ForPhysicalExamination = nameof(ForPhysicalExamination);
        public static readonly string ForBloodCollection = nameof(ForBloodCollection);
        public static readonly string ForMethodBloodCollection = nameof(ForMethodBloodCollection);
        public static readonly string ForLaboratoryTest = nameof(ForLaboratoryTest);
        public static readonly string Success = nameof(Success);
        public static readonly string Deferred = nameof(Deferred);
        public static readonly string Inventory = nameof(Inventory);
        public static readonly string ForCounseling = nameof(ForCounseling);
        public static readonly string ForVitalSigns = nameof(ForVitalSigns);
        public static readonly string ForConsent = nameof(ForConsent);
        public static readonly string ForBloodIssuance = nameof(ForBloodIssuance);
        public static readonly string ForPostDonationCare = nameof(ForPostDonationCare);
    }

    public static class BloodBagCollectionTypes
    {
        public static readonly string KARMI = nameof(KARMI);
        public static readonly string TERUMO = nameof(TERUMO);
        public static readonly string SpecialBag = nameof(SpecialBag);
        public static readonly string Apheresis = nameof(Apheresis);
    }

    public static class Roles
    {
        public static readonly string Admin = nameof(Admin);
        public static readonly string DonorAdmin = nameof(DonorAdmin);
        public static readonly string InitialScreener = nameof(InitialScreener);
        public static readonly string PhysicalExamScreener = nameof(PhysicalExamScreener);
        public static readonly string BloodCollector = nameof(BloodCollector);
    }

    public static class VitalSignType 
    { 
        public static readonly string PreTransfusion = nameof(PreTransfusion);
        public static readonly string DuringTransfusion = nameof(DuringTransfusion);
        public static readonly string PostTransfusion = nameof(PostTransfusion);
    }

    public static class TransfusionStatus
    {
        public static readonly string Completed = nameof(Completed);
        public static readonly string Stopped = nameof(Stopped);
    }

    public static class CompatibilityOptions
    {
        public static readonly string Compatible = nameof(Compatible);
        public static readonly string Incompatible = nameof(Incompatible);
        public static readonly string Blank = nameof(Blank);
        public static readonly string Doubtful = nameof(Doubtful);
    }

}
