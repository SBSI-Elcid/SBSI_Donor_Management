export class DonorReportFiltersDto {
    BloodTypes: Array<string> = [];
    Statuses: Array<string> = [];
    DonationDateFrom: Date | null = null;
    DonationDateTo: Date | null = null;
}