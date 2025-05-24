import { IDonorIssuedBloodBags} from '../DonorScreening/DonorIssuedBloodBags'

export interface IDonorBloodBagIssuance {
    DonorTransactionId: Guid,
    DonorRegistrationId: Guid,
    BloodBagInfos: IDonorIssuedBloodBags[]; 
    DonorStatus: string,
}

export class DonorBloodBagIssuanceDto implements IDonorBloodBagIssuance {
    DonorTransactionId: Guid = "";
    DonorRegistrationId: Guid = "";
    BloodBagInfos: IDonorIssuedBloodBags[] = [];
    DonorStatus: string = "";
}