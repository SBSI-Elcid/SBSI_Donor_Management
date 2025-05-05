import { RequestResult } from "@/common/RequestResult";
import { AuthenticationResult } from "@/models/AuthenticationResult";
import { LoginDto } from "@/models/LoginDto";
import { ApiClient } from "./ApiClient";

export default class AuthService {
	private apiClient: ApiClient;
	
	constructor() {
		this.apiClient = new ApiClient('api/authenticate/');
	}
	
	public async authenticate(loginDto: LoginDto): Promise<RequestResult<AuthenticationResult>> {
		return this.apiClient.postJson('', loginDto);
	}
}