export interface IVitalSignsMonitoringDetailsDto {
    VitalSignsMonitoringId: number;
    DonorPostDonationCareId: Guid;
    Time: Date | null,
    BP: string;
    PR: string;
    Others: string;       
}

export class VitalSignsMonitoringDetailsDto implements IVitalSignsMonitoringDetailsDto {
    VitalSignsMonitoringId: number = 0;
    DonorPostDonationCareId: string  = "";
    Time: Date | null = null
    BP: string = "";
    PR: string = "";
    Others: string = "";

}
