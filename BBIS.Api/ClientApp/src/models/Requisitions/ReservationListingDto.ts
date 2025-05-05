export class ReservationListingDto {
    ReservationId: Guid = '';
    PatientId: Guid = '';
    PatientName: string = '';
    Age: number = 0;
    BloodType: string = '';
    Components: Array<string> = [];
    TestOrderSummary: Array<RequestedTestOrderSummaryDto> = [];
    RequestedDate: Date = new Date();
    Status: string = '';
}

export class RequestedTestOrderSummaryDto {
    TestOrderGroupId: Guid = '';
    TestOrders: Array<string> = [];
    CrossMatchTestOrders: Array<string> = [];
    TestCompleted: boolean = false;
}