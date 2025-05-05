export interface InventoryListDto {
    InventoryItemId: Guid;
    BloodComponentId: Guid;
    ComponentName: string;
    BloodType: string;
    BloodRh: string;
    UnitSerialNumber: string;
    NoOfUnit: string;
    CollectionDate: Date;
    ExpiryDate: Date;
    Status: string;
}