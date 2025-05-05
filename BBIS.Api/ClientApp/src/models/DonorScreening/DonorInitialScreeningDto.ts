import { DonorRecentDonationDto, IDonorRecentDonationDto } from "./DonorRecentDonationDto";

export interface IDonorInitialScreeningDto {
    DonorTransactionId: Guid,
    DonorRegistrationId: Guid,
    Weight: number,
    SPGR: number,
    HGB: number,
    HCT: number,
    RBC: number,
    WBC: number,
    PLTCount: number,
    BloodType: string,
    DonationType: string,
    InHouseTypeValue: string,
    MobileBloodDonationPlace: string,
    MobileBloodDonationOrganizer: string,
    NameOfPatient: string,
    PatientHospital: string,
    PatientBloodType: string,
    PatientWBOrComponent: string,
    PatientNoOfUnits: number,
    HasRecentDonations: string,
    RecentDonations: Array<IDonorRecentDonationDto>,
    DonorStatus: string,
    DeferralStatus: string,
    Remarks: string,
    DonorName: string,
    HasTestOrder: boolean,
    TestCompleted: boolean,
    BloodDonator: string,
}
  
export class DonorInitialScreeningDto implements IDonorInitialScreeningDto {
    DonorTransactionId: Guid = "";
    DonorRegistrationId: Guid = "";
    Weight: number = 0;
    SPGR: number = 0;
    HGB: number = 0;
    HCT: number = 0;
    RBC: number = 0;
    WBC: number = 0;
    PLTCount: number = 0;
    BloodType: string = "";
    DonationType: string = "";
    InHouseTypeValue: string = "";
    MobileBloodDonationPlace: string = "";
    MobileBloodDonationOrganizer:string = "";
    NameOfPatient: string = "";
    PatientHospital: string = "";
    PatientBloodType: string = "";
    PatientWBOrComponent: string = "";
    PatientNoOfUnits: number = 0;
    HasRecentDonations: string = "";
    RecentDonations: Array<IDonorRecentDonationDto> = [];
    DonorStatus: string = "";
    DeferralStatus: string = "";
    Remarks: string = "";
    DonorName: string = "";
    HasTestOrder: boolean = false;
    TestCompleted: boolean = false;
    BloodDonator: string = "";
}
  