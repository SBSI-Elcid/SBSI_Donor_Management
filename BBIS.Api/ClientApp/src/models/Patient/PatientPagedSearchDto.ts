import { PagedSearchDto } from "../PagedSearchDto";

export class PatientPagedSearchDto extends PagedSearchDto {
    BloodType: string = '';
    BloodRh: string = '';
}