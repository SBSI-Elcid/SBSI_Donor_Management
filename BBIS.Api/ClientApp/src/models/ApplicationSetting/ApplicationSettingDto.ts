import { IApplicationSettingKeyValuePair } from "./IApplicationSettingKeyValuePair";

export interface IApplicationSettingDto extends IApplicationSettingKeyValuePair {
    ApplicationSettingId: Guid;
    IsActive: boolean;
}

export class ApplicationSettingDto implements IApplicationSettingDto {
    ApplicationSettingId: Guid = '';
    SettingKey: string = '';
    SettingValue: string = '';
    IsActive: boolean = false;
}