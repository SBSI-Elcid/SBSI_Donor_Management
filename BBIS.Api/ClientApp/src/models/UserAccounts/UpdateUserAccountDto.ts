import { IUserAccountBaseDto } from './IUserAccountBaseDto';

export interface IUpdateUserAccountDto extends IUserAccountBaseDto {
    UserAccountId: Guid | null;
    PasswordHasChange: boolean;
    UpdatedPassword: string | null;
    RoleIds: Array<Guid>;
    IsActive: boolean;
    UserModuleIds: Array<Guid>;
}

export class UpdateUserAccountDto implements IUpdateUserAccountDto {
    UserAccountId = null;
    PasswordHasChange = false;
    UpdatedPassword =  null;
    RoleIds = [];
    IsActive = false;
    Username = null;
    Firstname = null;
    Lastname = null;
    EmailAddress = null;
    EventRole = null;
    UserModuleIds = [];
}