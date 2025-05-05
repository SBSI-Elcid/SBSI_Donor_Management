import { TransfusionViewDto } from "./TransfusionViewDetailsDto";

export class TransfusionDto extends TransfusionViewDto {
    ReservationItemId: Guid = '';
    ReservationId: Guid = '';
}