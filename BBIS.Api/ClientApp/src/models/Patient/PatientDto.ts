export class PatientDto {
    PatientId: Guid | null = null;
    BloodType: string = '';
    Rh: string = '';
    PatientIdNo: string = '';
    LastName: string = '';
    FirstName: string = '';
    MiddleName: string = '';
    Gender: string = '';
    CivilStatus: string = '';
    DateOfBirth: Date | null = null;
    ContactNumber: string = '';
    Address: string = '';
}
