export interface IDonorLaboratoryTestDto {
    DonorTransactionId: Guid,
    BloodType: string,
    DonorStatus: string
    DeferralStatus: string,
    Remarks: string
}

export class DonorLaboratoryTestDto implements IDonorLaboratoryTestDto {
    DonorTransactionId: Guid = "";
    BloodType: string = "";
    DonorStatus: string = "";
    DeferralStatus: string = "";
    Remarks: string = "";
}