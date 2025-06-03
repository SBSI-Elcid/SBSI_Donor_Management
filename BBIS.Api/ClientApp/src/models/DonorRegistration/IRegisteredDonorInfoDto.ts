import { IDonorDto,DonorDto } from "./DonorDto";

export interface IRegisteredDonorInfoDto extends IDonorDto {
  DonorTransactionId: Guid;
  DonorStatus: string,
  DonorRegistrationId: Guid,
  RegistrationNumber: string,
}

export class RegisteredDonorInfoDto extends DonorDto implements IRegisteredDonorInfoDto {

    DonorTransactionId: Guid = "";
    DonorStatus: string = "";
    DonorRegistrationId: Guid = "";
    RegistrationNumber: string = "";
}