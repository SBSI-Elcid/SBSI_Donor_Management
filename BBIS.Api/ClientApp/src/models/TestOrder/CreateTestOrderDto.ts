import { CreateCrossMatchOrderDto } from './CreateCrossMatchOrderDto';
export class CreateTestOrderDto {
    PatientId: Guid = '';
    ReservationId: Guid = '';
    TestOrders: Array<Guid> = [];
    CrossMatchTestOrders: Array<CreateCrossMatchOrderDto> = [];
}