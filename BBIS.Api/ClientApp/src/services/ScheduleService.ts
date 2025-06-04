import { ApiClient } from "./ApiClient";
import { ISchedule, ScheduleDto } from "../models/Schedules/ScheduleDto"
import { SchedulePagedSearchDto } from "../models/Schedules/SchedulePagedSearchDto";
import { PagedSearchResultDto } from "../models/PagedSearchDto";
import { IChecklistDto } from "../models/Schedules/ChecklistDto";


export default class ScheduleService {
    baseUrl: string = 'api/schedule/';
    apiClient: ApiClient;

    constructor() {
        this.apiClient = new ApiClient(this.baseUrl);
    }
     async getSchedules(dto: SchedulePagedSearchDto): Promise<PagedSearchResultDto<ISchedule>> {
         return  this.apiClient.getPostData<PagedSearchResultDto<ISchedule>>(`schedule`, dto);
     
    }

    async getSchedulesById(id: Guid): Promise<ISchedule> {
        return this.apiClient.get<ISchedule>(`schedule/${id}`);
    }

    async getCheckList(id: Guid): Promise<IChecklistDto> {
        return this.apiClient.get<IChecklistDto>(`checklist/${id}`);
    }

    async upsertSchedule(dto: ISchedule): Promise<Guid> {
        let response = await this.apiClient.postJson(`upsert-schedule`, dto);
        return response.Data as Guid;
    }

    async upsertCheckList(dto: IChecklistDto): Promise<Guid> {
        let response = await this.apiClient.postJson(`upsert-checklist`, dto);
        return response.Data as Guid;
    }
   
}