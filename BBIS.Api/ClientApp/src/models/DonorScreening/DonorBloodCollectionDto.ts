export interface IDonorBloodCollectionDto {
    DonorBloodCollectionId: Guid | null,
    DonorTransactionId: Guid,
    //CollectionType: string,
    //CollectionSubType: string,
    CollectedBloodAmount: number | null,
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
    DonorBloodCollectionId: Guid | null = null;
    DonorTransactionId: Guid = "";
    //CollectionType: string = "";
    //CollectionSubType: string = "";
    CollectedBloodAmount: number | null = null;
    UnitOfMeasurement: string = "";
    Success: boolean = false;
    UnwellReason: string = "";
   /* MedicationGiven: string = "";*/
    StartTime: Date | null = null;
    EndTime: Date | null = null;
    PatientFirstName: string = "";
    PatientLastName: string = "";
    PatientMiddleName: string = "";
    PRCBloodDonorNumber: string = "";
    DonorStatus: string = "";
    DeferralStatus: string = "";
    Remarks: string = "";
    UnitSerialNumber: string = "";
}