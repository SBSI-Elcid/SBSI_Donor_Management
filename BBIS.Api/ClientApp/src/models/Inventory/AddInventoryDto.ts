import { InventoryItemDto } from "./InventoryItemDto";

export interface AddInventoryDto {
    DonorTranctionId: Guid | null;
    InstitutionId: Guid | null;
    InventoryItems: Array<InventoryItemDto>;
}