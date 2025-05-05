import { ApiClient } from "./ApiClient";
import { ILookup } from "@/models/Lookups/LookupDto";

export default class LookupService {
	baseUrl: string = 'api/lookups/';
	apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient(this.baseUrl);
	}
	
	async getLookupByKeys(keys: Array<string>): Promise<Array<ILookup>> {
        return this.apiClient.getPostData<Array<ILookup>>('bylookupkeys', { LookupKeys: keys });
	}
}