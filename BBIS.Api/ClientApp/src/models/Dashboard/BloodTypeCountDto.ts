export interface IBloodTypeCountDto {
    BloodType: string;
    Count: number;
}

export class BloodTypeCountDto implements IBloodTypeCountDto {
    BloodType: string = '';
    Count: number = 0;
}