export interface IVitalSignsMonitoringDetailsDto {
    VitalSignsMonitoringId: Guid | null;
    DonorPostDonationCareId: Guid | null;
    Time: Date | null,
    BP: number;
    PR: number;
    Others: string;       
}

export class VitalSignsMonitoringDetailsDto implements IVitalSignsMonitoringDetailsDto {
    VitalSignsMonitoringId: string | null = "";
    DonorPostDonationCareId: string | null = "";
    Time: Date | null = null
    BP: number = 0;
    PR: number = 0;
    Others: string = "";

}
