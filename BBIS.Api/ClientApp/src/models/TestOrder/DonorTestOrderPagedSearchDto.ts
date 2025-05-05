import { PagedSearchDto } from "../PagedSearchDto";

export class DonorTestOrderPagedSearchDto extends PagedSearchDto {
    DateFrom: Date | null = null;
    DateTo: Date | null = null;
}