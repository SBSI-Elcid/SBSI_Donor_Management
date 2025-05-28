export interface IPostDonationDetail {
    PostDonationDetailsId: number;
    Details: string,
    MonitoredBy: string;
}

export class PostDonationDetailsDto implements IPostDonationDetail {
    PostDonationDetailsId: number = 0;
    Details: string = "";
    MonitoredBy: string = "";

}
