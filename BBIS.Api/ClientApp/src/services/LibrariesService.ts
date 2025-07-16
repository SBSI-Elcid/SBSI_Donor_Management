import { ApiClient } from "./ApiClient";
import { ISchedule, ScheduleDto } from "../models/Schedules/ScheduleDto"
import { IActivityDonorDto, ActivityDonorDto } from "../models/Schedules/ActivityDonorDto"
import { SchedulePagedSearchDto } from "../models/Schedules/SchedulePagedSearchDto";
import { PagedSearchDto, PagedSearchResultDto } from "../models/PagedSearchDto";
import { IChecklistDto } from "../models/Schedules/ChecklistDto";
import { ActivityDonorPagedSearchDto } from "../models/Schedules/ActivityDonorPagedSearchDto";
import { IRoleDto } from "../models/ApplicationSetting/RoleDto";

export default class LibrariesService {
    baseUrl: string = 'api/libraries/';
    apiClient: ApiClient;

    constructor() {
        this.apiClient = new ApiClient(this.baseUrl);
    }
    async getLibrariesRoleSettings(dto: PagedSearchDto): Promise<PagedSearchResultDto<IRoleDto>> {
        return this.apiClient.getPostData<PagedSearchResultDto<IRoleDto>>(`libraries-role`, dto);
    }
}