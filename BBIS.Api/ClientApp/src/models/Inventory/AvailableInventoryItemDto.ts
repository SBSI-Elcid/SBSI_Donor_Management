export class AvailableInventoryItemDto {
    BloodComponentId: Guid = '';
    AvailableOptions: Array<InventoryUnitOptionsDto> = [];
}

export class InventoryUnitOptionsDto {
    InventoryItemId: Guid = '';
    ItemText: string = '';
}