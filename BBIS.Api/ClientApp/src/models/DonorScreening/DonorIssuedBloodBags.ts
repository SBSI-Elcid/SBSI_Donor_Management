export interface IDonorIssuedBloodBags {
    BloodBagToBeUsed: string,
    BloodBagType: string,
    UnitSerialNumber: string,
    SegmentSerialNumber: string,
    isFromModal: boolean,
    
}

export class DonorIssuedBloodBagsDto implements IDonorIssuedBloodBags {
    BloodBagToBeUsed: string = "";
    BloodBagType: string = "";
    UnitSerialNumber: string = "";
    SegmentSerialNumber: string = "";
    isFromModal: boolean = false;
}