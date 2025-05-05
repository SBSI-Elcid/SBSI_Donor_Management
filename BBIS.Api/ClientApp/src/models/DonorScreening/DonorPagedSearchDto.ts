import { PagedSearchDto } from "../PagedSearchDto";

export class DonorPagedSearchDto extends PagedSearchDto {
    Name: string = '';
    RegistrationNumber: string = '';
    BloodType: string = '';
    DonorStatus: string = '';
}