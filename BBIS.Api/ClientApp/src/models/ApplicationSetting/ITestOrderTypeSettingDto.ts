export interface ITestOrderTypeSettingDto {
    TestOrderTypeId: Guid;
    Code: string;
    Name: string;
    Description: string;
    IsActive: boolean;
}

export class TestOrderTypeSettingDto implements ITestOrderTypeSettingDto {
    TestOrderTypeId: Guid = '';
    Code: string = '';
    Name: string = '';
    Description: string = '';
    IsActive: boolean = true;
}