import { PagedSearchDto } from "../PagedSearchDto";

export class RequestedTestOrderPagedSearchDto extends PagedSearchDto {
    DateFrom: Date | null = null;
    DateTo: Date | null = null;
}