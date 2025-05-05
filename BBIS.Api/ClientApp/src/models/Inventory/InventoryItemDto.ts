export interface InventoryItemDto {
    BloodComponentId: Guid;
    BloodComponentName: string;
    UnitSerialNumber: string;
    BloodType: string;
    BloodRh: string;
    Volume: number;
    UnitMeasure: string;
    CollectionDate: Date;
    ExpiryDate: Date;
    NotifyBeforeExpireOn: Date;
}
