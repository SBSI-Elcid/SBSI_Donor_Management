export class ReservationItemDto {
    BloodComponentId: Guid | null= null;
    InventoryItemId: Guid | null= null;
    Volume: number = 0;
    OtherNotes: string = '';
    ReservationCheckLists: Array<Guid> = [];
}