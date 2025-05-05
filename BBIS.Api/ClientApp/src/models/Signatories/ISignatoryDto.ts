export interface ISignatoryDto {
    SignatoryId: Guid;
    FirstName: string;
    MiddleName: string;
    LastName: string;
    Position: string;
    PositionPrefix: string;
    LicenseNo: string;
    IsActive: boolean;
    IsPathologist: boolean;
}

export default class SignatoryDto implements ISignatoryDto{
    SignatoryId: Guid = '';
    FirstName: string = '';
    MiddleName: string = '';
    LastName: string = '';
    Position: string = '';
    PositionPrefix: string = '';
    LicenseNo: string = '';
    IsActive: boolean = true;
    IsPathologist: boolean = false;
}