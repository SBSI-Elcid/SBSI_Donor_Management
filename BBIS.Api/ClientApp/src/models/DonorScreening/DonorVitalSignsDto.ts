export interface IDonorVitalSigns {
    DonorTransactionId: Guid,
    DonorRegistrationId: Guid,
    BodyWeight: number,
    Temperature: number,
    BloodPressure: string,
    PulseRate: number,
    RespiratoryRate: number,
    OxygenSaturation: number,
    DonorStatus: string,
    DeferralStatus: string,
    Remarks: string
}

export class DonorVitalSignsDto implements IDonorVitalSigns {
    DonorTransactionId: Guid = "";
    DonorRegistrationId: Guid = "";
    BodyWeight: number = 0;
    Temperature: number = 0;
    BloodPressure: string = "";
    PulseRate: number = 0;
    RespiratoryRate: number = 0;
    OxygenSaturation: number = 0;
    DonorStatus: string = "";
    DeferralStatus: string = "";
    Remarks: string = "";
}