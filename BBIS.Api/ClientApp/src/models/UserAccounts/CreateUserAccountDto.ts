import { IUserAccountBaseDto } from "./IUserAccountBaseDto";

export interface ICreateUserAccountDto extends IUserAccountBaseDto {
    Password: string | null;
    RoleIds: Array<Guid>;
    UserModuleIds: Array<Guid>;
}

export default class CreatUserAccountDto implements ICreateUserAccountDto {
    Username = null;
    Firstname = null;
    Lastname = null;
    EmailAddress = null;
    Password = null;
    RoleIds = [];
    UserModuleIds = [];
}