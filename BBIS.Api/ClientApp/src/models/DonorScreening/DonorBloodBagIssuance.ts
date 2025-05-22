export interface IDonorBloodBagIssuance {
    DonorTransactionId: Guid,
    DonorRegistrationId: Guid,
    BloodBagToBeUsed: string,
    BloodBagType: string,
    UnitSerialNumber: string,
    SegmentSerialNumber: string,
    PatientFirstName: string,
    PatientLastName: string,
    PatientMiddleName: string,
    DonorStatus: string,
}

export class DonorBloodBagIssuanceDto implements IDonorBloodBagIssuance {
    DonorTransactionId: Guid = "";
    DonorRegistrationId: Guid = "";
    BloodBagToBeUsed: string = "";
    BloodBagType: string = "";
    UnitSerialNumber: string = "";
    SegmentSerialNumber: string = "";
    PatientFirstName: string = "";
    PatientLastName: string = "";
    PatientMiddleName: string = "";
    DonorStatus: string = "";
}