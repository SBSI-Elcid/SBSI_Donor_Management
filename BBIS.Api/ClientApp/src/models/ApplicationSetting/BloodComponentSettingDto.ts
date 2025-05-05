export interface IBloodComponentSettingDto {
    BloodComponentId: Guid;
    ComponentName: string;
    ExpiryInDays: number;
    NotifyOnDaysBefore: number;
    IsActive: boolean;
}

export class BloodComponentSettingDto implements IBloodComponentSettingDto {
    BloodComponentId: Guid = '';
    ComponentName: string = '';
    ExpiryInDays: number = 0;
    NotifyOnDaysBefore: number = 0;
    IsActive: boolean = true;
}