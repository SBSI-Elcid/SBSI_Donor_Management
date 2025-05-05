import { DonorTestOrderDto } from "./DonorTestOrderDto";

export interface IDonorTestOrderListDto extends DonorTestOrderDto {
    RegistrationNumber: string;
    FullName: string;
    IsReactive: boolean;
    DateCreated: Date;
}