import { DonorDto } from "./DonorDto";

export interface IVerifyDonorResultDto {
    IsValid: boolean,
    DeferralStatus: string,
    Remarks: string | null,
    Donor: DonorDto,
    donorRegistrationId: Guid

}
  
export class VerifyDonorResultDto implements IVerifyDonorResultDto {
    IsValid: boolean = false;
    DeferralStatus: string = "";
    Remarks: string | null = null;
    Donor: DonorDto = new DonorDto();
    donorRegistrationId: Guid = "";
}
  