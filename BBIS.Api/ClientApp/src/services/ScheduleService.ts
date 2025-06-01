import { ApiClient } from "./ApiClient";
import { ISchedule, ScheduleDto } from "../models/Schedules/ScheduleDto"
import { SchedulePagedSearchDto } from "../models/Schedules/SchedulePagedSearchDto";
import { PagedSearchResultDto } from "../models/PagedSearchDto";


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

    async upsertSchedule(dto: ISchedule): Promise<Guid> {
        let response = await this.apiClient.postJson(`upsert-schedule`, dto);
        return response.Data as Guid;
    }
   
}