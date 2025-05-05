import { IDonorDto } from "./DonorDto";

export interface IRegisteredDonorInfoDto extends IDonorDto {
  DonorTransactionId: Guid;
  DonorStatus: string,
  DonorRegistrationId: Guid,
  RegistrationNumber: string,
}