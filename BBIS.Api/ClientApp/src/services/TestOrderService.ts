import { RequestResult } from "@/common/RequestResult";
import { PagedSearchResultDto } from "@/models/PagedSearchDto";
import { CreateTestOrderDto } from "@/models/TestOrder/CreateTestOrderDto";
import { TestOrderDto } from "@/models/TestOrder/TestOrderDto";
import { TypeAheadResultDto } from "@/models/TestOrder/TypeAheadResultDto";
import { UpdateTestOrderDto } from "@/models/TestOrder/UpdateTestOrderDto";
import { IRequestedTestOrderListDto } from "@/models/TestOrder/IRequestedTestOrderListDto";
import { BloodTestTypeDto } from "@/models/TestOrder/BloodTestTypeDto";
import { CreateDonorTestOrderDto } from "@/models/TestOrder/CreateDonorTestOrderDto";
import { DonorTestOrderDto } from "@/models/TestOrder/DonorTestOrderDto";
import { IDonorTestOrderListDto } from "@/models/TestOrder/DonorTestOrderListDto";
import { TestOrderTypeDto } from "@/models/TestOrder/TestOrderTypeDto";
import { ApiClient } from "./ApiClient";
import { DonorTestOrderPagedSearchDto } from "@/models/TestOrder/DonorTestOrderPagedSearchDto";
import { RequestedTestOrderPagedSearchDto } from "@/models/TestOrder/RequestedTestOrderPagedSearchDto";


export default class TestOrderService {
	baseUrl: string = 'api/TestOrders/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}

	async getBloodTestTypes(): Promise<Array<BloodTestTypeDto>> {
		return this.apiClient.get<Array<BloodTestTypeDto>>(`blood-testtypes`);
	}

	async getDonorTestOrderList(dto: DonorTestOrderPagedSearchDto): Promise<PagedSearchResultDto<IDonorTestOrderListDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IDonorTestOrderListDto>>(`donor-testorders`, dto);
	}

	async getRequestedTestOrders(dto: RequestedTestOrderPagedSearchDto): Promise<PagedSearchResultDto<IRequestedTestOrderListDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IRequestedTestOrderListDto>>(`requested-testorders`, dto);
	}

	async getDonorTestOrder(id: Guid): Promise<DonorTestOrderDto> {
		return this.apiClient.get<DonorTestOrderDto>(`donor-testorder/${id}`);
	}

	async createDonorTestOrder(dto: CreateDonorTestOrderDto): Promise<Guid> {
		let response = await this.apiClient.postJson(`create-donor-testorder`, dto);
		return response.Data as Guid; 
	}

	async updateDonorTestOrder(dto: DonorTestOrderDto): Promise<boolean> {
		let response = await this.apiClient.postJson(`update-donor-testorder`, dto);
		return response.Data as boolean; 
	}

	// test order group
	async createTestOrder(dto: CreateTestOrderDto): Promise<Guid> {
		let response = await this.apiClient.postJson(`create-testorder`, dto);
		return response.Data as Guid; 
	}

	async updateTestOrder(dto: UpdateTestOrderDto): Promise<boolean> {
		let response = await this.apiClient.postJson(`update-testorder`, dto);
		return response.Data as boolean; 
	}

	async getTestOrder(id: Guid): Promise<TestOrderDto> {
		return this.apiClient.get<TestOrderDto>(`get-testorder/${id}`);
	}

	async getTestOrderStatus(id: Guid): Promise<boolean> {
		return this.apiClient.get<boolean>(`get-testorderstatus/${id}`);
	}

	async deleteTestOrder(id: Guid): Promise<RequestResult> {
		return this.apiClient.deleteJson(`delete-testorder/${id}`, null);
	}

	async getTestOrderTypes(): Promise<Array<TestOrderTypeDto>> {
		return this.apiClient.get<Array<TestOrderTypeDto>>(`testordertypes`);
	}

	async searchDonors(searchText: string, bt: string): Promise<Array<TypeAheadResultDto>> {
		return this.apiClient.get<Array<TypeAheadResultDto>>(`search-donors?st=${searchText}&bt=${bt}`);
	}

	async searchPatients(searchText: string): Promise<Array<TypeAheadResultDto>> {
		return this.apiClient.get<Array<TypeAheadResultDto>>(`search-patients?st=${searchText}`);
	}
}