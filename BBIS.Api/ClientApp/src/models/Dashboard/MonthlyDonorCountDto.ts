export interface IMonthlyDonorCountDto {
    Month: string;
    Count: number;
}

export class MonthlyDonorCountDto implements IMonthlyDonorCountDto {
    Month: string = "";
    Count: number = 0;
}