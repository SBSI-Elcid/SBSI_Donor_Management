import { IVitalSignsMonitoringDetailsDto } from "./VitalSignsMonitoringDetailsDto";

export interface IDonorPostDonationCare {
    DonorPostDonationCareId: Guid | null;
    DonorTransactionId: Guid | null;
    TypeOfReaction: string;
    SeverityOfReaction: string;
    ReactionManifestation: string;
    ActionInterventions: string;
    DoctorsNote: string;
    DischargeStatus: string;
    MonitoredBy: Guid | null;
    DoctorName: string;
    DischargeDate: Date | null;

    VitalSignsMonitoringDetails: IVitalSignsMonitoringDetailsDto[];

}
export class DonorPostDonationCareDto implements IDonorPostDonationCare {
    DonorPostDonationCareId: string | null = "";
    DonorTransactionId: string | null = "";
    TypeOfReaction: string = "";
    SeverityOfReaction: string = "";
    ReactionManifestation: string = "";
    ActionInterventions: string = "";
    DoctorsNote: string = "";
    DischargeStatus: string = "";
    MonitoredBy: string | null = "";
    DoctorName: string = "";
    DischargeDate: Date | null = null
    VitalSignsMonitoringDetails: IVitalSignsMonitoringDetailsDto[] = [];

}