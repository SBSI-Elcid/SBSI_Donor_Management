//export enum TabNames {
//    DonorRegistration = "Donor Registration",
//    DonorVitalSigns = "Vital Signs",
//    DonorInformation = "Donor Information",
//    InitialScreening = "Initial Screening",
//    PhysicalExam = "Physical Exam",
//    Counseling = "Counseling",
//    ConsentForm = "Consent Form",
//    MethodBloodCollection = "Method Blood Collection",
//    IssuanceOfBloodBag = "Issuance of Blood Bag",
//    BloodCollection = "Blood Collection",
//    PostDonationCare = "Post Donation Care",
//    LaboratoryTest = "Laboratory Test",
//    VerifyDonorInfo = "Verify Donor Information"
//}
export enum TabNames {
    DonorVitalSigns = "DonorVitalSigns",
    DonorInformation = "DonorInformation",
    InitialScreening = "InitialScreening",
    PhysicalExam = "PhysicalExam",
    Counseling = "Counseling",
    ConsentForm = "ConsentForm",
    MethodBloodCollection = "MethodBloodCollection",
    IssuanceOfBloodBag = "IssuanceOfBloodBag",
    BloodCollection = "BloodCollection",
    PostDonationCare = "PostDonationCare",
    Deferred = "Deferred"
}

export const TabAliases: Record<TabNames, string> = {
    [TabNames.DonorVitalSigns]: "ForVitalSigns",
    [TabNames.DonorInformation]: "ForDonorInformation",
    [TabNames.InitialScreening]: "ForInitialScreening",
    [TabNames.PhysicalExam]: "ForPhysicalExamination",
    [TabNames.Counseling]: "ForCounseling",
    [TabNames.ConsentForm]: "ForConsent",
    [TabNames.MethodBloodCollection]: "ForMethodBloodCollection",
    [TabNames.IssuanceOfBloodBag]: "ForBloodIssuance",
    [TabNames.BloodCollection]: "ForBloodCollection",
    [TabNames.PostDonationCare]: "ForPostDonationCare",
    [TabNames.Deferred]: "Deferred",
};