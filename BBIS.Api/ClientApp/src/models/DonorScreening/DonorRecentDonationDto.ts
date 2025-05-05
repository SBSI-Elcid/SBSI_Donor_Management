export interface IDonorRecentDonationDto {
    DonorRecentDonationId: Guid | null;
    NumberOfDonation: number | null;
    DateOfRecentDonation: Date | null;
    PlaceOfRecentDonation: string;
    Agency: string;
}
  
export class DonorRecentDonationDto implements IDonorRecentDonationDto {
    DonorRecentDonationId: Guid | null = '';
    NumberOfDonation: number | null = null;
    DateOfRecentDonation: Date | null = null;
    PlaceOfRecentDonation: string = "";
    Agency: string = "";
}