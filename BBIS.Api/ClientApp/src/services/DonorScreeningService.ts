import { ApiClient } from "./ApiClient";
import { IDonorInitialScreeningDto } from "@/models/DonorScreening/DonorInitialScreeningDto";
import { IDonorPhysicalExaminationDto } from "@/models/DonorScreening/DonorPhysicalExaminationDto";
import { IDonorBloodCollectionDto } from "@/models/DonorScreening/DonorBloodCollectionDto";
import { PagedSearchResultDto } from "@/models/PagedSearchDto";
import { IDonorListDto } from "@/models/DonorScreening/IDonorListDto";
import { DonorPagedSearchDto } from "@/models/DonorScreening/DonorPagedSearchDto";
import { IDonorRecentDonationDto } from "@/models/DonorScreening/DonorRecentDonationDto";

export default class DonorScreeningService {
	baseUrl: string = 'api/donorscreening/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}

	async getDonors(dto: DonorPagedSearchDto): Promise<PagedSearchResultDto<IDonorListDto>> {
		return this.apiClient.getPostData<PagedSearchResultDto<IDonorListDto>>(`donors`, dto);
	}

	async getInitialScreeningInfo(id: Guid): Promise<IDonorInitialScreeningDto> {
		return this.apiClient.get<IDonorInitialScreeningDto>(`initialscreening/${id}`);
	}

	async getRecentDonations(id: Guid): Promise<Array<IDonorRecentDonationDto>> {
		return this.apiClient.get<Array<IDonorRecentDonationDto>>(`initialscreening/recentdonation/${id}`);
	}

	async getPhysicalExamInfo(id: Guid): Promise<IDonorPhysicalExaminationDto> {
		return this.apiClient.get<IDonorPhysicalExaminationDto>(`physicalexam/${id}`);
	}

	async getBloodCollectionInfo(id: Guid): Promise<IDonorBloodCollectionDto> {
		return this.apiClient.get<IDonorBloodCollectionDto>(`bloodcollection/${id}`);
	}

	async upsertInitialScreening(dto: IDonorInitialScreeningDto): Promise<Guid> {
		let response = await this.apiClient.postJson(`upsert-initialscreening`, dto);
		return response.Data as Guid; 
	}

	async upsertPhysicalExamination(dto: IDonorPhysicalExaminationDto): Promise<Guid> {
		let response = await this.apiClient.postJson(`upsert-physicalexamination`, dto);
		return response.Data as Guid; 
	}

	async upsertBloodColllection(dto: IDonorBloodCollectionDto): Promise<Guid> {
		let response = await this.apiClient.postJson(`upsert-bloodcollection`, dto);
		return response.Data as Guid; 
	}
}