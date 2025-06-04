export interface IChecklistDto {
    ChecklistId: number;
    ScheduleId?: string; // Guid -> string
    ConfirmationCall: string;
    VerificationCall: string;
    FinalCall: string;
    UpdatedAt: Date;
    UpdatedBy: string; // Guid -> string
}
export class ChecklistDto implements IChecklistDto {
    ChecklistId: number = 0;
    ScheduleId?: string = "";
    ConfirmationCall: string = "";
    VerificationCall: string = "";
    FinalCall: string = "";
    UpdatedAt: Date = new Date();
    UpdatedBy: string = "";
}