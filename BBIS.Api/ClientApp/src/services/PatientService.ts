import { PatientResult } from './../models/Patient/PatientResult';
import { PatientDto } from './../models/Patient/PatientDto';
import { PatientViewDto } from './../models/Patient/PatientViewDto';
import { PagedSearchResultDto } from './../models/PagedSearchDto';
import { ApiClient } from "./ApiClient";
import { PatientPagedSearchDto } from '@/models/Patient/PatientPagedSearchDto';

export default class UserAccountsService {
	baseUrl: string = 'api/patients/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}
	
	async getList(dto: PatientPagedSearchDto): Promise<PagedSearchResultDto<PatientViewDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<PatientViewDto>>(`list`, dto);
	}

	async getPatientById(id: Guid): Promise<PatientDto> {
		return this.apiClient.get<PatientDto>(`${id}`);
	}
	
	async create(dto: PatientDto): Promise<PatientResult> {
		return this.apiClient.getPostData<PatientResult>(`create`, dto);
	}

	async update(dto: PatientDto): Promise<PatientResult> {
		return this.apiClient.getPostData<PatientResult>(`update`, dto);
	}

	// async delete(id: Guid): Promise<RequestResult> {
	// 	return this.apiClient.deleteJson(`${id}`, null);
	// }
}