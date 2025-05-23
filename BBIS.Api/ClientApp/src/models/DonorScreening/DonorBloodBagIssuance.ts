export interface IDonorBloodBagIssuance {
    DonorTransactionId: Guid,
    DonorRegistrationId: Guid,
    BloodBagToBeUsed: string,
    BloodBagType: string,
    UnitSerialNumber: string,
    SegmentSerialNumber: string,
    isFromModal: boolean,
    DonorStatus: string,
}

export class DonorBloodBagIssuanceDto implements IDonorBloodBagIssuance {
    DonorTransactionId: Guid = "";
    DonorRegistrationId: Guid = "";
    BloodBagToBeUsed: string = "";
    BloodBagType: string = "";
    UnitSerialNumber: string = "";
    SegmentSerialNumber: string = "";
    isFromModal: boolean = false;
    DonorStatus: string = "";
}