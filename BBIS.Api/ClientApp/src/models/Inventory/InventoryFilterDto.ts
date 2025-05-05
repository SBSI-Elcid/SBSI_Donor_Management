export interface InventoryFilterDto {
    BloodTypes: Array<string>;
    Statuses: Array<string>;
    DateFilterType: string;
    DateFrom: Date | null;
    DateTo: Date | null;
}