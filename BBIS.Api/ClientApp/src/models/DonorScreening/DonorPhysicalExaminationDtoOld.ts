export interface IDonorPhysicalExaminationDto {
    DonorTransactionId: Guid,
    BloodPressure: string,
    Pulse: number | null,
    Temperature: number | null,
    GeneralStatus: string,
    Skin: string,
    HEENT: string,
    HeartAndLungs: string,
    ResultStatus: string,
    FailedRemarks: string | null,
    BloodBagType: string,
    DoctorName: string,
    DonorStatus: string,
    DeferralStatus: string,
    Remarks: string | null
}
  
export class DonorPhysicalExaminationDto implements IDonorPhysicalExaminationDto {
    DonorTransactionId: Guid = "";
    BloodPressure: string = "";
    Pulse: number | null = null;
    Temperature: number | null = null;
    GeneralStatus: string = "";
    Skin: string = "";
    HEENT: string = "";
    HeartAndLungs: string = "";
    ResultStatus: string = "";
    FailedRemarks: string | null = null;
    BloodBagType: string = "";
    DoctorName: string = "";
    DonorStatus: string = "";
    DeferralStatus: string = "";
    Remarks: string = "";
}