import { PagedSearchDto } from "../PagedSearchDto";

export interface SchedulePagedSearchDto extends PagedSearchDto {
    ScheduleId?: Guid;
    ActivityName?: string;
    ActivityType?: string;
    DateFrom?: Date;
    DateTo?: Date;
}

