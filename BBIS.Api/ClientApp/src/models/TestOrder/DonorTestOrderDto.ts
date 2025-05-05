import { DonorTestOrderTypesDto } from "./DonorTestOrderTypesDto";

export interface DonorTestOrderDto {
    DonorTestOrderId: Guid;
    DonorTransactionId: Guid;
    TestCompleted: boolean;
    TestTypes: Array<DonorTestOrderTypesDto>;
    FinalBloodType: string;
    BloodRh: string;
}