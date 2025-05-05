import { ModuleDto } from './../models/UserAccounts/ModuleDto';
import { IUpdateUserAccountDto } from './../models/UserAccounts/UpdateUserAccountDto';
import { IUserAccountViewDto } from './../models/UserAccounts/IUserAccountViewDto';
import { PagedSearchDto, PagedSearchResultDto } from './../models/PagedSearchDto';
import { ApiClient } from "./ApiClient";
import { ICreateUserAccountDto } from '@/models/UserAccounts/CreateUserAccountDto';
import { RequestResult } from '@/common/RequestResult';
import { IUserProfileUpdateDto } from '@/models/UserAccounts/UserProfileUpdateDto';

export default class UserAccountsService {
	baseUrl: string = 'api/useraccounts/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}
	
	async getUserList(dto: PagedSearchDto): Promise<PagedSearchResultDto<IUserAccountViewDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IUserAccountViewDto>>(`list`, dto);
	}

	async getUser(id: Guid): Promise<IUserAccountViewDto> {
		return this.apiClient.get<IUserAccountViewDto>(`${id}`);
	}

	async createUser(dto: ICreateUserAccountDto): Promise<RequestResult<number>> {
		return this.apiClient.postJson(``, dto);
	}

	async updateUser(dto: IUpdateUserAccountDto): Promise<RequestResult> {
		return this.apiClient.putJson(``, dto);
	}

	async deleteUser(id: Guid): Promise<RequestResult> {
		return this.apiClient.deleteJson(`${id}`, null);
	}

	async updateProfile(dto: IUserProfileUpdateDto): Promise<RequestResult> {
		return this.apiClient.postJson(`update-profile`, dto);
	}

	async getUserModules(id: Guid): Promise<Array<ModuleDto>> {
		return this.apiClient.get<Array<ModuleDto>>(`modules/${id}`);
	}

	async getAllModules(): Promise<Array<ModuleDto>> {
		return this.apiClient.get<Array<ModuleDto>>(`all-modules`);
	}
}