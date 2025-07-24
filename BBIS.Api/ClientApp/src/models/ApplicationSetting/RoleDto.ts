import { IUserRoleScreeningAccessDto } from "./IUserRoleScreeningAccessDto";

export interface IRoleDto {
   RoleId:string
   RoleName:string

    UserRoleAccesses: IUserRoleScreeningAccessDto[];
}

export class RoleDto implements IRoleDto {
    RoleId:string = ""
    RoleName: string = ""
    UserRoleAccesses: IUserRoleScreeningAccessDto[] = [];
}