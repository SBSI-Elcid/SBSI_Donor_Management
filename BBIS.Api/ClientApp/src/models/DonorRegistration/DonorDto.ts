import { DonorMedicalHistoryDto } from "./DonorMedicalHistoryDto";

export interface IDonorDto {
    Firstname: string,
    Lastname: string,
    Middlename: string,
    BirthDate: Date | null,
    CivilStatus: string,
    Age: number | null,
    Gender: string,
    AddressNo: string,
    AddressStreet: string,
    AddressBarangay: string,
    AddressMunicipality: string,
    AddressProvinceOrCity: string,
    AddressZipcode: string,
    OfficeAddress: string,
    WorkName: string,
    Religion: string,
    Nationality: string,
    Education: string,
    TelNo: string,
    MobileNo: string,
    EmailAddress: string,
    SchoolIdNo: string | null,
    CompanyIdNo: string | null,
    PRCNo: string | null,
    DriverLicenseNo: string | null,
    SssGsisBirNo: string | null,
    OtherNo: string | null,
    MedicalHistories: Array<DonorMedicalHistoryDto>

}

export class DonorDto implements IDonorDto{
    Firstname: string = "";
    Lastname: string = "";
    Middlename: string = "";
    BirthDate: Date | null = null;
    CivilStatus: string = "";
    Age: number | null = null;
    Gender: string = "";
    AddressNo: string = "";
    AddressStreet: string = "";
    AddressBarangay: string = "";
    AddressMunicipality: string = "";
    AddressProvinceOrCity: string = "";
    AddressZipcode: string = "";
    OfficeAddress: string = "";
    WorkName: string = "";
    Religion: string = "";
    Nationality: string = "";
    Education: string = "";
    TelNo: string = "";
    MobileNo: string = "";
    EmailAddress: string = "";
    SchoolIdNo: string | null = null;
    CompanyIdNo: string | null = null;
    PRCNo: string | null = null;
    DriverLicenseNo: string | null = null;
    SssGsisBirNo: string | null = null;
    OtherNo: string | null = null;
    MedicalHistories: Array<DonorMedicalHistoryDto> = [];
}