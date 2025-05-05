import { InventoryItemViewDto } from "./InventoryItemViewDto";

export interface InventoryGroupDto {
    BloodType: string;
    BloodRh: string;
    TotalNumberOfUnits: number;
    TotalExpiredCount: number;
    InventoryItems: Array<InventoryItemViewDto>;
}