export interface ISchedule {
    ScheduleId: Guid;
    Status: string;
    ActivityName: string;
    ActivityType: string;
    ScheduleDateTime: Date;
    ActivityVenue: string;
    PartnerInstitutionName: string;
    PointPersonName: string;
    ExpectedDonorNumber: number;
}

export class ScheduleDto implements ISchedule {
    ScheduleId: Guid = "";
    Status: string = "";
    ActivityName: string = "";
    ActivityType: string = "";
    ScheduleDateTime: Date = new Date();
    ActivityVenue: string = "";
    PartnerInstitutionName: string = "";
    PointPersonName: string = "";
    ExpectedDonorNumber: number = 0;
}