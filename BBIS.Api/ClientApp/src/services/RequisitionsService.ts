import { ApiClient } from "./ApiClient";
import { ChecklistOptionDto } from "@/models/Requisitions/ChecklistOptionDto";
import { ReservationDto } from "@/models/Requisitions/ReservationDto";
import { ReservationListingDto } from "@/models/Requisitions/ReservationListingDto";
import { PagedSearchDto, PagedSearchResultDto } from "@/models/PagedSearchDto";
import { UpdateReservationDto } from "@/models/Requisitions/UpdateReservationDto";
import { TransfusionDto } from "@/models/Requisitions/TransfusionDto";
import { TransfusionViewDetailsDto } from "@/models/Requisitions/TransfusionViewDetailsDto";
import { GroupCrossMatchOrderDto } from '@/models/TestOrder/GroupCrossMatchOrderDto';

export default class RequisitionsService {
	baseUrl: string = 'api/requisitions/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}

	async getChecklists(): Promise<Array<ChecklistOptionDto>> {
		return this.apiClient.get<Array<ChecklistOptionDto>>(`check-list`);
	}

	async createReservation(dto: ReservationDto): Promise<Guid>  {
		let response = await this.apiClient.postJson(`create`, dto);
		return response.Data as Guid; 
	}

	async updateReservation(dto: UpdateReservationDto): Promise<Guid>  {
		let response = await this.apiClient.postJson(`update`, dto);
		return response.Data as Guid; 
	}

	async getReservations(dto: PagedSearchDto): Promise<PagedSearchResultDto<ReservationListingDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<ReservationListingDto>>(`reservations`, dto);
	}

	async getTransfusionDetails(id: Guid): Promise<TransfusionViewDetailsDto> {
		return this.apiClient.get<TransfusionViewDetailsDto>(`transfusion/${id}`);
	}

	async upsertTransfusionDetails(dto: Array<TransfusionDto>): Promise<Guid> {
		let response = await this.apiClient.postJson(`transfusion/upsert`, dto);
		return response.Data as Guid; 
	}

	async getItemsForCrossMatching(id: Guid): Promise<Array<GroupCrossMatchOrderDto>> {
		return this.apiClient.get<Array<GroupCrossMatchOrderDto>>(`reservation-donors/${id}`);
	}
}