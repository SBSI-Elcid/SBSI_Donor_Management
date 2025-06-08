export interface IActivityDonorDto {
    ActivityDonorId: number;
    ScheduleId: Guid;
    LastName: string;
    FirstName: string;
    MiddleName: string;
    FullName: string;
    
}

export class ActivityDonorDto implements IActivityDonorDto {
    ActivityDonorId: number = 0;
    ScheduleId: Guid = "";
    LastName: string = "";
    FirstName: string = "";
    MiddleName: string = "";
    FullName: string = "";

}