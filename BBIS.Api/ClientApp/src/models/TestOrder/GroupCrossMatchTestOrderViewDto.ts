import { CrossMatchTestOrderDto } from "./CrossMatchTestOrderDto";

export interface GroupCrossMatchTestOrderViewDto {
    BloodComponentId: Guid;
    BloodComponentName: string;
    CrossMatchTestOrders: Array<CrossMatchTestOrderDto>;
}