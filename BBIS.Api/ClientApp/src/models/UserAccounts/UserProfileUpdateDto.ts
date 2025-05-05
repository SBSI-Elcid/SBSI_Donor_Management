export interface IUserProfileUpdateDto {
    UserId: Guid;
    Username: string | null;
    CurrentPassword: string;
    NewPassword: string;
 }

export default class UserProfileUpdateDto implements IUserProfileUpdateDto{
    UserId: Guid = '';
    Username: string | null = '';
    CurrentPassword: string = '';
    NewPassword: string = '';
 }