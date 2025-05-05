import { ApiClient } from "./ApiClient";
import { RequestResult } from '@/common/RequestResult';

export default class SyncDataService {
	baseUrl: string = 'api/syncdata/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}
	
	async syncDonor(): Promise<string> {
		return this.apiClient.getPostData<string>(`sync-donors`, null);
	}
}