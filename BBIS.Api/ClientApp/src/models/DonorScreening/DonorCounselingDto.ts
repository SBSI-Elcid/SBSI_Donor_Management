import { IDonorMedicalHistoryDto } from "../DonorRegistration/DonorMedicalHistoryDto";

export interface IDonorCounseling {
    
    DonorStatus: string,
    DonorRegistrationId: Guid,
    medicalHistories: IDonorMedicalHistoryDto[];
    DeferralStatus: string,
    Remarks: string
}

export class DonorCounselingDto implements IDonorCounseling {
    DonorStatus: string = "";
    DonorRegistrationId: Guid = "";
    medicalHistories: IDonorMedicalHistoryDto[] = [];
    DeferralStatus: string = "";
    Remarks: string = "";
}