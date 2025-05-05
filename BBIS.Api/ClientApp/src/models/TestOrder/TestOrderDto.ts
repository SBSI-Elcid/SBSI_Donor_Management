import { BloodScreeningTestOrderDto } from "./BloodScreeningTestOrderDto";
import { BloodTypingTestOrderDto } from "./BloodTypingTestOrderDto";
import { CoombsTestOrderDto } from "./CoombsTestOrderDto";
import { CrossMatchTestOrderDto } from "./CrossMatchTestOrderDto";
import { GroupCrossMatchTestOrderViewDto } from "./GroupCrossMatchTestOrderViewDto";

export interface TestOrderDto {
    TestOrderId: Guid;
    PatientId: Guid | null;
    PatientName: string;
    ReservationId: Guid | null;
    TestCompleted: boolean;
    Remarks: string;
    PerformedBy: Guid | null;
    ReviewedBy: Guid | null;
    ValidatedBy: Guid | null;
    PathologistId: Guid | null;
    CoombsTestOrder: CoombsTestOrderDto | null;
    BloodScreeningTestOrder: BloodScreeningTestOrderDto | null;
    BloodTypingTestOrder: BloodTypingTestOrderDto | null;
    GroupCrossMatchTestOrders: Array<GroupCrossMatchTestOrderViewDto>;
}