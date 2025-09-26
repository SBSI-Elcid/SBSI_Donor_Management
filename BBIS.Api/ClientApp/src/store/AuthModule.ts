import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import BrowserStorageService from '../services/BrowserStorageService';
import { StorageKeysEnum } from '../common/StorageKeysEnum';
import { JwtData } from '../models/JwtData';
import jwt_decode from "jwt-decode";
import Common from '@/common/Common';
import { Roles } from '@/models/Enums/Roles';
import { ModuleDto } from '@/models/UserAccounts/ModuleDto';
import { AuthenticationResult } from '@/models/AuthenticationResult';

@Module({ name: 'auth' })
export default class AuthModule extends VuexModule {
  private jwtToken: string = "";
  private _refreshToken: string = "";
  private storageService: Storage = new BrowserStorageService();
  private _userModules: Array<ModuleDto> = [];
  private _userId: Guid = '';
  private _authResult: AuthenticationResult | null = null;
  
  @Mutation
  protected SET_USER_MODULES(modules: Array<ModuleDto>): void {   
    this._userModules = modules;
    this.storageService.setItem(StorageKeysEnum.UseModules, JSON.stringify(modules));
  }

  @Mutation
  protected SET_AUTH_RESULT(authResult: AuthenticationResult): void {   
    this._userId = authResult.UserInfo.UserAccountId;
    this.jwtToken = authResult.Token; 

    if (this._userId) {
      this.storageService.setItem(StorageKeysEnum.UserId, this._userId);
    }

    if (authResult.Token) {
      this.storageService.setItem(StorageKeysEnum.Token, authResult.Token);
    }
    this._refreshToken = authResult.RefreshToken; 
    if (this._refreshToken) {
      this.storageService.setItem(StorageKeysEnum.RefreshToken, this._refreshToken);
    } 
  }

  @Mutation
  protected SET_AUTH_TOKEN(token: string): void {   
    this.jwtToken = token; 
    if (token) {
      this.storageService.setItem(StorageKeysEnum.Token, token);
    } else {
      this.storageService.removeItem(StorageKeysEnum.Token);
    }
  }

  @Mutation
  protected SET_REFRESH_TOKEN(token: string): void {   
    this._refreshToken = token; 
    if (token) {
      this.storageService.setItem(StorageKeysEnum.RefreshToken, token);
    } else {
      this.storageService.removeItem(StorageKeysEnum.RefreshToken);
    }
  }

  @Mutation
  public REMOVE_TOKEN(): void {
    this.jwtToken = "";
    this._refreshToken = "";
    this._userId = "";
    this._authResult = null;
    this._userModules = [];
  }

  @Action({ commit: 'SET_USER_MODULES' })
  public async setUserModules(modules: Array<ModuleDto>): Promise<Array<ModuleDto>> {
    return modules;
  }

  @Action({ commit: 'SET_AUTH_RESULT' })
  public async setAuthenticationResult(auth: AuthenticationResult): Promise<AuthenticationResult> {
    return auth;
  }
  
  @Action({ commit: 'REMOVE_TOKEN', rawError: true })
  public async signOut(): Promise<void> {
    return;
  }

  public get authToken(): string {
    const token: any = this.storageService.getItem(StorageKeysEnum.Token);
    const tokenValue = this.jwtToken ? this.jwtToken : String(token);
    return tokenValue;
  }

  public get refreshToken(): string {
    let token: any = this.storageService.getItem(StorageKeysEnum.RefreshToken);
    const tokenValue = this._refreshToken ? this._refreshToken : String(token);
    return tokenValue;
  }

  public get isAuthenticated(): boolean {    
    let token: any = this.storageService.getItem(StorageKeysEnum.Token);
    return token ? true : false;
  }

  public get userJwtData(): JwtData {
    if (!this.isAuthenticated) {
      return { Name: '', roles: [], UserId: 0, Username: ''};
    }
    const data = jwt_decode(this.authToken) as JwtData;
    return data;
  }

  public get userRoles(): Array<string> {
    let userRoles = this.userJwtData.roles;
    if (Common.isArray(userRoles)) {
      return userRoles as Array<string>;
    }
    else {
      let role = userRoles as string;
      return [role];
    }
  }

  public get userHasRole(): (role: string) => boolean {
    return (role) => this.isAuthenticated && this.userRoles.some(r => r === role || r === Roles.Admin);
  }

  //public get userHasAnyRole(): (roles: Array<string>) => boolean {
  //  return (roles) => {
  //    roles.push(Roles.Admin);
  //    return this.isAuthenticated && this.userRoles.some(userRole => roles.includes(userRole));
  //  }
  //  }
    public get userHasAnyRole(): (roles: Array<string>) => boolean {
        return (roles) => {
            const rolesToCheck = [...roles, Roles.Admin]; // only for permission check
            return this.isAuthenticated && this.userRoles.some(userRole => rolesToCheck.includes(userRole));
        }
    }   

  public get authenticated(): boolean {
    return !this.tokenExpired;
  }

  public get tokenExpired(): boolean {
    if (!this.authToken) {
      return true;
    }
    const data: any = jwt_decode(this.authToken);
    let current_time = Date.now() / 1000;
    return data.exp < current_time;
  }

  public get userModules(): Array<ModuleDto> {
    let modules: Array<ModuleDto> = this._userModules;

    if(!modules) {
      const itemValue = this.storageService.getItem(StorageKeysEnum.UseModules);
      if (itemValue) {
        modules = JSON.parse(StorageKeysEnum.UseModules) as Array<ModuleDto>;
      }
    }
    return modules;
  }

  public get userId(): string {
    const id: any = this.storageService.getItem(StorageKeysEnum.UserId);
    const actualId = this._userId ? this._userId : String(id);
    return actualId;
  }

}