import { IRegisteredDonorDto } from "../DonorRegistration/IRegisteredDonorDto";

export interface IDonorListDto extends IRegisteredDonorDto {
    BirthDate: Date,
    ContactNo: string,
    BloodType: string,
    DonorStatus: string
    DonorTransactionId: Guid;
    HasTestOrder: boolean;
    TestOrderCompleted: boolean;
    IsOffline: boolean;
    IsSync: boolean;
}