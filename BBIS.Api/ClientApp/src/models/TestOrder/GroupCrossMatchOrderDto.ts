import { CreateCrossMatchOrderDto } from './CreateCrossMatchOrderDto';
export interface GroupCrossMatchOrderDto {
    BloodComponentId: Guid;
    BloodComponentName: string;
    CrossMatchTestOrders: Array<CreateCrossMatchOrderDto>;
    CrossMatchType: string;
}