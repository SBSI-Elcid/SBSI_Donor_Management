export interface IUserRoleScreeningAccessDto {
    UserRoleScreeningAccessId: string;
    RoleId: string;
    ScreeningTabName: string;
}

export class UserRoleScreeningAccessDto implements IUserRoleScreeningAccessDto {
    UserRoleScreeningAccessId: string = "";
    RoleId: string = "";
    ScreeningTabName: string = "";

}
