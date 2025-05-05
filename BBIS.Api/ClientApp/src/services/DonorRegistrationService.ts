import { ApiClient } from "./ApiClient";
import { IVerifyDonorResultDto } from "@/models/DonorRegistration/VerifyDonorResultDto";
import { IVerifyDonorDto } from "@/models/DonorRegistration/IVerifyDonorDto";
import { IMedicalQuestionnaireDto } from "@/models/DonorRegistration/MedicalQuestionnaireDto";
import { IDonorDto } from "@/models/DonorRegistration/DonorDto";
import { IRegisteredDonorDto } from "@/models/DonorRegistration/IRegisteredDonorDto";
import { PagedSearchDto, PagedSearchResultDto } from "@/models/PagedSearchDto";
import { IRegisteredDonorInfoDto } from "@/models/DonorRegistration/IRegisteredDonorInfoDto";

export default class DonorRegistrationService {
	baseUrl: string = 'api/donorregistration/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}

	async getMedicalQuestions(): Promise<Array<IMedicalQuestionnaireDto>> {
    	return this.apiClient.get<Array<IMedicalQuestionnaireDto>>('questionnaires');
	}

    async getRegisteredDonors(dto: PagedSearchDto): Promise<PagedSearchResultDto<IRegisteredDonorDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IRegisteredDonorDto>>(`registereddonors`, dto);
	}

    async getRegisteredDonorInfo(regId: Guid): Promise<IRegisteredDonorInfoDto> {
		return this.apiClient.get<IRegisteredDonorInfoDto>(`info/${regId}`);
	}

	async verifyDonor(statusCheck: IVerifyDonorDto): Promise<IVerifyDonorResultDto> {
    	return this.apiClient.getPostData<IVerifyDonorResultDto>(`verify`, statusCheck);
	}

	async register(dto: IDonorDto): Promise<string> {
		let response = await this.apiClient.postJson(`register`, dto);
		return response.Data as string;
	}

	async updateCreateDonorTransaction(dto: IDonorDto, registrationId: Guid): Promise<Guid> {
		let paramDto = dto as IRegisteredDonorInfoDto;
		paramDto.DonorRegistrationId = registrationId;

		let response = await this.apiClient.postJson(`updatecreate-transaction`, paramDto);
		return response.Data as Guid; 
	}
}