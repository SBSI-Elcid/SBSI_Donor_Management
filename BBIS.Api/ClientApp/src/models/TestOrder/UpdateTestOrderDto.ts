import { BloodTypingTestOrderDto } from './BloodTypingTestOrderDto';
import { BloodScreeningTestOrderDto } from './BloodScreeningTestOrderDto';
import { CoombsTestOrderDto } from './CoombsTestOrderDto';
import { UpdateCrossMatchTestOrderDto } from './UpdateCrossMatchTestOrderDto';

export class UpdateTestOrderDto {
    TestOrderId: Guid = '';
    PatientId: Guid | null = null;
    ReservationId: Guid | null = null;
    TestCompleted: boolean = false;
    Remarks: string = '';
    PerformedBy: Guid | null = null;
    ReviewedBy: Guid | null = null;
    ValidatedBy: Guid | null = null;
    PathologistId: Guid | null = null;
    CrossMatchTestOrders: Array<UpdateCrossMatchTestOrderDto> = [];
    CoombsTestOrder: CoombsTestOrderDto | null = null;
    BloodScreeningTestOrder: BloodScreeningTestOrderDto | null = null;
    BloodTypingTestOrder: BloodTypingTestOrderDto | null = null;
}