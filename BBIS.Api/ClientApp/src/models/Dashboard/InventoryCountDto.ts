export interface IInventoryCountDto {
    TotalItems: number;
    TotalNearlyExpiredItems: number;
    TotalExpiredItems: number;
}

export class InventoryCountDto implements IInventoryCountDto {
    TotalItems: number = 0;
    TotalNearlyExpiredItems: number = 0;
    TotalExpiredItems: number = 0;
}