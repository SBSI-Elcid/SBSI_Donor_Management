import { CrossMatchTestOrderViewDto } from './CrossMatchTestOrderViewDto';
import { IRequestedTestOrderDto } from "./IRequestedTestOrderDto";

export interface IRequestedTestOrderListDto {
    TestOrderId: Guid;
    ReservationId: Guid;
    PatientName: string;
    TestOrderNumber: string;
    TestCompleted: boolean;
    Remarks: string;
    TestOrdersResult: Array<IRequestedTestOrderDto>;
    CrossMatchTestOrdersResult: Array<CrossMatchTestOrderViewDto>;
    DateCreated: Date;
}