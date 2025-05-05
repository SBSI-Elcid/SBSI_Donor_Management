import { PagedSearchDto } from "../PagedSearchDto";

export class RequisitionPagedSearchDto extends PagedSearchDto {
    Statuses: Array<string> = [];
    DateFrom: Date | null = null;
    DateTo: Date | null = null;
}