export interface IInstitutionDto {
    InstitutionId: Guid;
    Name: string;
    Address: string;
    ContactNo: string;
    EmailAddress: string;
    IsActive: boolean;
}

export class InstitutionDto implements IInstitutionDto {
    InstitutionId: Guid = '';
    Name: string = '';
    Address: string = '';
    ContactNo: string = '';
    EmailAddress: string = '';
    IsActive: boolean = true;
}