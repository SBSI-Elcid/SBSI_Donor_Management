export interface IVitalSignsMonitoringDetailsDto {
    VitalSignsMonitoringId: number;
    DonorPostDonationCareId: Guid;
    Time: Date | null,
    BP: number;
    PR: number;
    Others: string;       
}

export class VitalSignsMonitoringDetailsDto implements IVitalSignsMonitoringDetailsDto {
    VitalSignsMonitoringId: number = 0;
    DonorPostDonationCareId: string  = "";
    Time: Date | null = null
    BP: number = 0;
    PR: number = 0;
    Others: string = "";

}
