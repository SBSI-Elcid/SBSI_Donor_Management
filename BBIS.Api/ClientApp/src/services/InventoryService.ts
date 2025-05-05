import { InventoryListDto } from './../models/Inventory/InventoryListDto';
import { AddInventoryDto } from "@/models/Inventory/AddInventoryDto";
import { InventoryFilterDto } from "@/models/Inventory/InventoryFilterDto";
import { InventoryGroupDto } from "@/models/Inventory/InventoryGroupDto";
import { InventoryItemDto } from "@/models/Inventory/InventoryItemDto";
import { ApiClient } from "./ApiClient";
import { AvailableInventoryItemDto } from '@/models/Inventory/AvailableInventoryItemDto';
import { HttpMethodEnum } from '@/common/HttpMethodEnum';

export default class InventoryService {
	baseUrl: string = 'api/inventory/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}
	
	async prepareInventoryItems(transactionId: Guid): Promise<Array<InventoryItemDto>> {
		return this.apiClient.get<Array<InventoryItemDto>>(`prepareInventoryItems/${transactionId}`);
	}

	async addToInventory(dto: AddInventoryDto): Promise<void> {
		await this.apiClient.postJson(`addToInventory`, dto);
	}

	async getInventoryStatus(): Promise<Array<InventoryGroupDto>> {
		return this.apiClient.get<Array<InventoryGroupDto>>(`status`);
	}

	async getInventoryItems(dto: InventoryFilterDto): Promise<Array<InventoryListDto>> {
		return await this.apiClient.getPostData<Array<InventoryListDto>>(`get-items`, dto);
	}

	async getAvailableInventoryUnits(bloodType: string, rh: string): Promise<Array<AvailableInventoryItemDto>> {
		return this.apiClient.get<Array<AvailableInventoryItemDto>>(`availableUnits/${bloodType}/${rh}`);
	}

    async exportInventory(filters: InventoryFilterDto): Promise<any> {
		return this.apiClient.downloadFile<any>(HttpMethodEnum.Post, `export-inventory`, filters);
	}
}