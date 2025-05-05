import { SignatoryOptionDto } from './../models/Signatories/SignatoryOptions';
import { PagedSearchDto, PagedSearchResultDto } from './../models/PagedSearchDto';
import { ApiClient } from "./ApiClient";
import { RequestResult } from '@/common/RequestResult';
import SignatoryViewDto from '@/models/Signatories/SignatoryViewDto';
import { ISignatoryDto } from '@/models/Signatories/ISignatoryDto';

export default class SignatoryService {
	baseUrl: string = 'api/signatory/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}
	
	async getSignatories(dto: PagedSearchDto): Promise<PagedSearchResultDto<SignatoryViewDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<SignatoryViewDto>>(`list`, dto);
	}

	async getSignatory(id: Guid): Promise<ISignatoryDto> {
		return this.apiClient.get<ISignatoryDto>(`${id}`);
	}

	async upsertSignatory(dto: ISignatoryDto): Promise<RequestResult<number>> {
		return this.apiClient.postJson(``, dto);
	}

	async deleteSignatory(id: Guid): Promise<RequestResult> {
		return this.apiClient.deleteJson(`${id}`, null);
	}

	async getSignatoryOptions(): Promise<Array<SignatoryOptionDto>> {
		return this.apiClient.get<Array<SignatoryOptionDto>>(`options`);
	}
}