import { ModuleDto } from './ModuleDto';
import { IUserAccountBaseDto } from "./IUserAccountBaseDto";

export interface IUserAccountViewDto extends IUserAccountBaseDto {
   UserAccountId: Guid;
   IsActive: boolean;
   RoleIds: Array<Guid>;
   UserModules: Array<ModuleDto>;
}