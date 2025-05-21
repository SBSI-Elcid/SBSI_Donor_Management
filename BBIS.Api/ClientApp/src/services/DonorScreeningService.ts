import { ApiClient } from "./ApiClient";
import { IDonorInitialScreeningDto } from "@/models/DonorScreening/DonorInitialScreeningDto";
import { IDonorPhysicalExaminationDto } from "@/models/DonorScreening/DonorPhysicalExaminationDto";
import { IDonorBloodCollectionDto } from "@/models/DonorScreening/DonorBloodCollectionDto";
import { PagedSearchResultDto } from "@/models/PagedSearchDto";
import { IDonorListDto } from "@/models/DonorScreening/IDonorListDto";
import { DonorPagedSearchDto } from "@/models/DonorScreening/DonorPagedSearchDto";
import { IDonorRecentDonationDto } from "@/models/DonorScreening/DonorRecentDonationDto";
import { IDonorVitalSigns } from "../models/DonorScreening/DonorVitalSignsDto";
import { IDonorCounseling } from "../models/DonorScreening/DonorCounselingDto";
import { IDonorMedicalHistoryDto } from "../models/DonorRegistration/DonorMedicalHistoryDto";
import { IRegisteredDonorDto } from "../models/DonorRegistration/IRegisteredDonorDto";
import { IRegisteredDonorInfoDto } from "../models/DonorRegistration/IRegisteredDonorInfoDto";

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

	async getDonorVitalSignsInfo(id: Guid): Promise<IDonorVitalSigns> {
		return this.apiClient.get<IDonorVitalSigns>(`vitalsigns/${id}`);
	}

	async getDonorCounselingInfo(id: Guid): Promise<IDonorVitalSigns> {
		return this.apiClient.get<IDonorVitalSigns>(`counseling/${id}`);
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
	async upsertVitalSignsScreening(dto: IDonorVitalSigns): Promise<Guid> {
		let response = await this.apiClient.postJson(`upsert-vitalsigns`, dto);
		return response.Data as Guid;
	}

	async uMethodBloodCollection(dto: IRegisteredDonorInfoDto): Promise<Guid> {
		let response = await this.apiClient.postJson(`u-methodbloodcollection`, dto);
		return response.Data as Guid;
	}
	async upsertCounselingScreening(dto: IDonorCounseling): Promise<Guid> {
		console.log(dto);
		let response = await this.apiClient.postJson(`upsert-counseling`, dto);
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