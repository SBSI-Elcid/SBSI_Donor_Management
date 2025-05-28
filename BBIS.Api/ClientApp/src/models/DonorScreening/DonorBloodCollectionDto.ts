export interface IDonorBloodCollectionDto {
    DonorBloodCollectionId: Guid,
    DonorTransactionId: Guid,
    //CollectionType: string,
    //CollectionSubType: string,
    CollectedBloodAmount: number,
    UnitOfMeasurement: string,
    Success: boolean,
    UnwellReason: string,
   /* MedicationGiven: string,*/
    StartTime: Date | null,
    EndTime: Date | null,
    PatientFirstName: string,
    PatientLastName: string,
    PatientMiddleName: string,
    PRCBloodDonorNumber: string,
    DonorStatus: string
    DeferralStatus: string,
    Remarks: string,
    UnitSerialNumber: string
}

export class DonorBloodCollectionDto implements IDonorBloodCollectionDto {
    DonorBloodCollectionId: Guid = "";
    DonorTransactionId: Guid = "";
    //CollectionType: string = "";
    //CollectionSubType: string = "";
    CollectedBloodAmount: number = 0;
    UnitOfMeasurement: string = "";
    Success: boolean = false;
    UnwellReason: string = "";
   /* MedicationGiven: string = "";*/
    StartTime: Date | null = null
    EndTime: Date | null  = null
    PatientFirstName: string = "";
    PatientLastName: string = "";
    PatientMiddleName: string = "";
    PRCBloodDonorNumber: string = "";
    DonorStatus: string = "";
    DeferralStatus: string = "";
    Remarks: string = "";
    UnitSerialNumber: string = "";
}