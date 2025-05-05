import { ReservationItemDto } from "./ReservationItemDto";

export class ReservationDto {
    PatientId: Guid | null= null;
    PatientType: string | null= null;
    PriorityLevel: string | null= null;
    ForAdult: boolean = true;
    IsEmergency: boolean = false;
    RoomNo: string = '';
    Membership: string = '';
    RequestedBy: string = '';
    RequestedDateTime: Date | null= null;
    PreviousTransfusionDate: Date | null= null;
    PreviousNoOfUnitsTransfused: number | null= null;
    ReservationItems: Array<ReservationItemDto> = [];
}