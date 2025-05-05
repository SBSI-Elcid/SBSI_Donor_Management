import { RequestResult } from "@/common/RequestResult";
import { IApplicationSettingDto } from "@/models/ApplicationSetting/ApplicationSettingDto";
import { IBloodComponentChecklistDto } from "@/models/ApplicationSetting/BloodComponentChecklistDto";
import { IBloodComponentSettingDto } from "@/models/ApplicationSetting/BloodComponentSettingDto";
import { IApplicationSettingKeyValuePair } from "@/models/ApplicationSetting/IApplicationSettingKeyValuePair";
import { IBloodComponentChecklistListDto } from "@/models/ApplicationSetting/IBloodComponentChecklistListDto";
import { IBloodComponentDto } from "@/models/ApplicationSetting/IBloodComponentDto";
import { IInstitutionDto } from "@/models/ApplicationSetting/InstitutionDto";
import { ITestOrderTypeSettingDto } from "@/models/ApplicationSetting/ITestOrderTypeSettingDto";
import { PagedSearchDto, PagedSearchResultDto } from "@/models/PagedSearchDto";
import { ApiClient } from "./ApiClient";

export default class ApplicationSettingService {
	baseUrl: string = 'api/applicationSetting/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}

	async getApplicationSettings(dto: PagedSearchDto): Promise<PagedSearchResultDto<IApplicationSettingDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IApplicationSettingDto>>(`settings`, dto);
	}

	async getBloodComponentSettings(dto: PagedSearchDto): Promise<PagedSearchResultDto<IBloodComponentSettingDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IBloodComponentSettingDto>>(`settings/bloodComponents`, dto);
	}

	async getInstitutionSettings(dto: PagedSearchDto): Promise<PagedSearchResultDto<IInstitutionDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IInstitutionDto>>(`settings/institutions`, dto);
	}

	async getBloodComponentChecklistSettings(dto: PagedSearchDto): Promise<PagedSearchResultDto<IBloodComponentChecklistListDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IBloodComponentChecklistListDto>>(`settings/checklists`, dto);
	}

	async getTestOrderTypeSettings(dto: PagedSearchDto): Promise<PagedSearchResultDto<ITestOrderTypeSettingDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<ITestOrderTypeSettingDto>>(`settings/testOrderTypes`, dto);
	}
	
	async getApplicationSettingsByKey(keys: Array<string>): Promise<Array<IApplicationSettingKeyValuePair>> {
        return this.apiClient.getPostData<Array<IApplicationSettingKeyValuePair>>('bysettingkeys', { SettingKeys: keys });
	}

	async getInstitutionById(id: Guid): Promise<IInstitutionDto> {
		return this.apiClient.get<IInstitutionDto>(`institution/${id}`);
	}

	async getBloodComponents(): Promise<Array<IBloodComponentDto>> {
		return this.apiClient.get<Array<IBloodComponentDto>>(`bloodComponents`);
	}

	async getBloodComponentChecklistById(id: Guid): Promise<IBloodComponentChecklistDto> {
		return this.apiClient.get<IBloodComponentChecklistDto>(`checklist/${id}`);
	}

	async getTestOrderTypeById(id: Guid): Promise<ITestOrderTypeSettingDto> {
		return this.apiClient.get<ITestOrderTypeSettingDto>(`testOrderType/${id}`);
	}

	async updateApplicationSetting(dto: IApplicationSettingDto): Promise<RequestResult> {
		return this.apiClient.putJson(``, dto);
	}

	async upsertBloodComponentSetting(dto: IBloodComponentSettingDto): Promise<Guid>  {
		let response = await this.apiClient.postJson(`upsert-bloodcomponent`, dto);
		return response.Data as Guid; 
	}

	async upsertInstitutionSetting(dto: IInstitutionDto): Promise<Guid>  {
		let response = await this.apiClient.postJson(`upsert-institution`, dto);
		return response.Data as Guid; 
	}

	async upsertBloodComponentChecklist(dto: IBloodComponentChecklistDto): Promise<Guid>  {
		let response = await this.apiClient.postJson(`upsert-checklist`, dto);
		return response.Data as Guid; 
	}

	async upsertTestOrderType(dto: ITestOrderTypeSettingDto): Promise<Guid>  {
		let response = await this.apiClient.postJson(`upsert-testOrderType`, dto);
		return response.Data as Guid; 
	}

	async getAppStatus(): Promise<boolean> {
		return this.apiClient.get<boolean>(`get-app-status`);
	}

}