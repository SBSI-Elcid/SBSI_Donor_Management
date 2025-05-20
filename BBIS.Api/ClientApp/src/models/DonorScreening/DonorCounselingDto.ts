import { IDonorMedicalHistoryDto } from "../DonorRegistration/DonorMedicalHistoryDto";

export interface IDonorCounseling {
    
    DonorStatus: string,
    DonorRegistrationId: Guid,
    medicalHistories: IDonorMedicalHistoryDto[];
}

export class DonorCounselingDto implements IDonorCounseling {
    DonorStatus: string = "";
    DonorRegistrationId: Guid = "";
    medicalHistories: IDonorMedicalHistoryDto[] = [];
}