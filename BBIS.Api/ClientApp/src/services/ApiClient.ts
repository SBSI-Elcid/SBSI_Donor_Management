import { RefreshTokenResult } from './../models/RefreshTokenResult';
import BrowserStorageService from './BrowserStorageService';
import { StorageKeysEnum } from '../common/StorageKeysEnum';
import { RequestResult } from "@/common/RequestResult";
import { HttpMethodEnum } from "@/common/HttpMethodEnum";
import axios, { AxiosHeaders, AxiosInstance, AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig } from 'axios';

export class ApiClient  {
  private baseUrl: string;
  private storageService = new BrowserStorageService();
  protected instance: AxiosInstance;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
    this.instance = axios.create({
      baseURL: baseUrl,
    });

    this._initializeRequestInterceptor();
    this._initializeResponseInterceptor();
  }

  private _initializeResponseInterceptor = () => {
    this.instance.interceptors.response.use(
      this._handleResponse,
      this._handleResponseError,
    );
  };

  private _initializeRequestInterceptor = () => {
    this.instance.interceptors.request.use(
      this._handleRequest,
      this._handleError,
    );
  };
 
  private _handleRequest = (config: InternalAxiosRequestConfig) => {
    config.headers['Authorization'] = 'Bearer ' + this.authToken;
    config.headers['Content-Type'] = 'application/json';
    return config;
  };

  private _handleResponse = ({ data }: AxiosResponse) => data;

  protected _handleError = (error: any) => Promise.reject(error);

  protected _handleResponseError = async (error: any) => {
    const originalRequest = error.config;

    // UnAuthorize status
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      await this._refreshToken();     

      axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.authToken;
      return this.instance(originalRequest);
    }
    // Forbidden status
    if (error.response.status === 403) {
      window.location.replace('#/forbidden');
    }

    return Promise.reject(error);
  }

  protected async _refreshToken(): Promise<void> {
    try {
      const renewTokenRequest = {
        AccessToken: this.authToken, // old access token,
        RefreshToken: this.getRefreshToken,
      };
      
      const result = await axios.post<RequestResult<RefreshTokenResult>>('api/authenticate/renew', renewTokenRequest);
      const tokenResult = result.data.Data as RefreshTokenResult;

      if (result.data.Success) {
         // update the tokens in the localStorage
        this.storageService.setItem(StorageKeysEnum.Token, tokenResult.Token);
        this.storageService.setItem(StorageKeysEnum.RefreshToken, tokenResult.RefreshToken);
      }
      else {
        this.storageService.clear();
        window.location.replace('/');
      }
    } 
    catch (error) {
      throw error;
    }
  }

  private get authToken(): string | null {
    const token = this.storageService.getItem(StorageKeysEnum.Token);
    return token;
  }

  private get getRefreshToken(): string | null {
    const token = this.storageService.getItem(StorageKeysEnum.RefreshToken);
    return token;
  }
  
  public async get<T = any>(url: string) : Promise<T> {
    return this.getData<T>(HttpMethodEnum.Get, url);
  }

  public async getPostData<T = any>(url: string, data?: any): Promise<T> {
    return this.getData<T>(HttpMethodEnum.Post, url, data);
  }

  public async getData<T = any>(method: HttpMethodEnum, url: string, data?: any) : Promise<T> {
    var result = this.sendRequest<T>(method, url, data);
    return this.checkResponseData(result);
  }

  public async getJson<T = any>(url: string, data?: any): Promise<RequestResult<T>> {
    return this.sendRequest<T>(HttpMethodEnum.Get, url, data);
  }

  public async postJson<T = any>(url: string, data: any): Promise<RequestResult<T>> {
    return this.sendRequest<T>(HttpMethodEnum.Post, url, data);
  }

  public async putJson<T = any>(url: string, data: any): Promise<RequestResult<T>> {
    return this.sendRequest<T>(HttpMethodEnum.Put, url, data);
  }

  public async patchJson<T = any>(url: string, data: any): Promise<RequestResult<T>> {
    return this.sendRequest<T>(HttpMethodEnum.Patch, url, data);
  }

  public deleteJson<T = any>(url: string, data: any): Promise<RequestResult<T>> {
    return this.sendRequest<T>(HttpMethodEnum.Delete, url, data);
  }
  
  public async downloadFile<T = any>(method: HttpMethodEnum, url: string, data?: any): Promise<any> {
    const requestConfig: AxiosRequestConfig<any> = 
      data ? { method, data, url, responseType: 'blob' } 
           : { method, url, responseType: 'blob' };
      
    return this.instance(requestConfig);
  }

  public async sendRequest<T = any>(method: HttpMethodEnum, url: string, data?: any): Promise<RequestResult<T>> {
    const requestConfig: AxiosRequestConfig<any> = 
      data ? { method: method, data: data, url: url } 
           : { method: method, url: url };
      
    return this.instance(requestConfig)
      .then(response => this.handleResponse<T>(response))
  }

  private async handleResponse<T>(response: any): Promise<RequestResult<T>> {
    let jsonResult = response as RequestResult<T>;
    return jsonResult;
  }
  
  private async checkResponseData<T>(result: Promise<RequestResult<T>>): Promise<T> {
    return result.then(result => {
      if (result.Data == null) {
        throw result;
      }
      return result.Data;
    });
  }
}