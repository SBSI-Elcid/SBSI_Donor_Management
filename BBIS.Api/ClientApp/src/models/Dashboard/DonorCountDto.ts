import { IMonthlyDonorCountDto } from "./MonthlyDonorCountDto";

export interface IDonorCountDto {
    Donors: Array<IMonthlyDonorCountDto>;
    DeferredDonors: Array<IMonthlyDonorCountDto>;
}