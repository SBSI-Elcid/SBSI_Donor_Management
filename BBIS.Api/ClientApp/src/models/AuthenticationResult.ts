import { UserInfo } from "./UserInfo";

export interface AuthenticationResult {
  Token: string,
  RefreshToken: string,
  Success: boolean,
  UserInfo: UserInfo
}