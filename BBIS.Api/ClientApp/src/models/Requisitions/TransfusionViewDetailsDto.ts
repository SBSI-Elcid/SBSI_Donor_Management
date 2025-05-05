export class TransfusionViewDetailsDto {
    ReservationId: Guid = '';
    ReservationStatus: string = '';
    PatientDetails: PatientDetailsDto = new PatientDetailsDto();
    BloodComponentDetails: Array<BloodComponentDetailsDto> = [];
}

export class PatientDetailsDto
{
    FullName: string = '';
    Age: number = 0;
    Gender: string = '';
    BloodType: string = '';
    BloodRh: string = '';
    PatientNo: string = '';
    RequestingPhysician: string = '';
    RoomNo: string = '';
    Membership: string = '';
    PreviousTransfusionDate: Date | null = null;
    PreviousNoOfUnitsTransfused: number | null = null;
}

export class BloodComponentDetailsDto
{
    ReservationItemId: Guid = '';
    ComponentName: string = '';
    ComponentBloodType: string = '';
    ComponentBloodRh: string = '';
    Volume: number = 0;
    UnitMeasure: string = '';
    UnitSerialNumber: string = '';
    ExpirationDate: Date = new Date();
    Tranfusion: TransfusionViewDto = new TransfusionViewDto();
}

export class TransfusionViewDto {
    TransfusionId: Guid | null = null;
    TransfusionStarted: Date | null = null;
    TransfusionEnded: Date | null = null;
    TotalTimeConsumed: number = 0;
    MedicationGiven: string = '';
    HookedBy: string = '';
    RemovedBy: string = '';
    VitalSigns: Array<TransfusionVitalSignDto> = [];
    TransfusionStatus: string = '';
    TransfusionNotes: string = '';
}

export class TransfusionVitalSignDto
{
    VitalSignId : Guid | null = null;
    VitalSignType: string = '';
    Temperature: number | null = null;
    BloodPressure: string = '';
    RespiratoryRate: number | null = null;
    PulseRate: number | null = null;
    Remarks: string = '';
}
