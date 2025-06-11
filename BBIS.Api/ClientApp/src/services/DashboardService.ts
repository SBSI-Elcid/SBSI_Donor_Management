import { ApiClient } from "./ApiClient";
import { IInventoryCountDto } from '@/models/Dashboard/InventoryCountDto';
import { IBloodTypeCountDto } from "@/models/Dashboard/BloodTypeCountDto";
import { IDonorCountDto } from "@/models/Dashboard/DonorCountDto";
import { ISchedule } from "../models/Schedules/ScheduleDto";

export default class DashboardService {
	baseUrl: string = 'api/dashboard/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}

	async getDonorCount(): Promise<number> {
		return this.apiClient.get<number>(`donorCount`);
	}

    async getInventoryCount(): Promise<IInventoryCountDto> {
		return this.apiClient.get<IInventoryCountDto>(`inventoryCount`);
	}

	async getScheduleList(): Promise<Array<ISchedule>> {
		return this.apiClient.get<Array<ISchedule>>(`scheduleList`);
	}

    async getBloodTypeCount(): Promise<Array<IBloodTypeCountDto>> {
		return this.apiClient.get<Array<IBloodTypeCountDto>>(`bloodTypeCount`);
	}

	async getMonthlyDonorCount(): Promise<IDonorCountDto> {
		return this.apiClient.get<IDonorCountDto>(`monthlyDonors`);
	}
}