import { PagedSearchDto } from "../PagedSearchDto";

export class ActivityDonorPagedSearchDto extends PagedSearchDto {
    ScheduleId?: Guid;
    LastName?: string;
    FirstName?: string;
    MiddleName?: Date;
    
}

