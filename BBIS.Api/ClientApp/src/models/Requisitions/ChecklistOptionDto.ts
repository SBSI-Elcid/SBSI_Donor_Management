import { CheckListDto } from "./CheckListDto";

export class ChecklistOptionDto {
    BloodComponentId: Guid = "";
    ComponentName: string = "";
    CheckLists: Array<CheckListDto> = [];
}