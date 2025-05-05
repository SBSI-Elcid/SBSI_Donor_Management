import { BarcodeResultDto } from './../models/Reports/BarcodeResultDto';
import { TestOrderReportDto } from './../models/Reports/TestOrderReportDto';
import { ApiClient } from "./ApiClient";
import { HttpMethodEnum } from "@/common/HttpMethodEnum";
import { DonorReportFiltersDto } from "@/models/Reports/DonorReportFiltersDto";
import { DonorReportDto } from "@/models/Reports/DonorReportDto";
import { GenerateBarcodeDto } from '@/models/Reports/GenerateBarcodeDto';

export default class ReportsService {
	baseUrl: string = 'api/reports/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}

    async exportDonorReport(filters: DonorReportFiltersDto): Promise<any> {
		return this.apiClient.downloadFile<any>(HttpMethodEnum.Post, `export-donor`, filters);
	}

    async getDonorReport(filters: DonorReportFiltersDto): Promise<Array<DonorReportDto>> {
		return this.apiClient.getPostData<Array<DonorReportDto>>(`report-donor`, filters);
	}

	async exportTestOrderReport(id: Guid): Promise<Response> {
		return this.apiClient.downloadFile<Response>(HttpMethodEnum.Post, `export-testorder-report?id=${id}`);
	}

	async getTestOrderReport(id: Guid): Promise<TestOrderReportDto> {
		return this.apiClient.get<TestOrderReportDto>(`testorder-report/${id}`);
	}

	async generateBarcodes(dto: GenerateBarcodeDto): Promise<Array<BarcodeResultDto>> {
		return this.apiClient.getPostData<Array<BarcodeResultDto>>(`generate-barcode`, dto);
	}
}