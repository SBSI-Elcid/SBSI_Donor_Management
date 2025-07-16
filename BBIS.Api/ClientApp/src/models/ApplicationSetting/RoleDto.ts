export interface IRoleDto {
   RoleId:string
   RoleName:string
}

export class RoleDto implements IRoleDto {
    RoleId:string = ""
    RoleName:string = ""
}