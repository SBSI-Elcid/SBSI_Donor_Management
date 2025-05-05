export interface CreateCrossMatchOrderDto {
    BloodComponentId: Guid;
    DonorTransactionId: Guid;
    BloodComponentName: string;
    DonorUnitSerialNumber: string;
    CrossMatchType: string;
    Result: string;
    Source: string;
    CollectionDate: Date;
    ExpiryDate: Date;
    Volume: string;
}
