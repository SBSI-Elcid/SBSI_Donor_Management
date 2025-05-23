import { DonorRecentDonationDto, IDonorRecentDonationDto } from "./DonorRecentDonationDto";

export interface IDonorInitialScreeningDto {
    DonorTransactionId: Guid,
    DonorRegistrationId: Guid,
    MethodOfBloodCollection:string,
    HGB: number,
    HCT: number,
    RBC: number,
    WBC: number,
    PLTCount: number,
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
    MethodOfBloodCollection: string = "";
    HGB: number = 0;
    HCT: number = 0;
    RBC: number = 0;
    WBC: number = 0;
    PLTCount: number = 0;
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
  