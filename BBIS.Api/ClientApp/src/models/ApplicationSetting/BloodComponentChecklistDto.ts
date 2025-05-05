export interface IBloodComponentChecklistDto {
    BloodComponentChecklistId: Guid;
    BloodComponentId: Guid;
    ChecklistDescription: string;
    IsActive: boolean;
    IsAdult: boolean;
}

export class BloodComponentChecklistDto implements IBloodComponentChecklistDto {
    BloodComponentChecklistId: Guid = '';
    BloodComponentId: Guid = '';
    ChecklistDescription: string = '';
    IsActive: boolean = true;
    IsAdult: boolean = true;
}