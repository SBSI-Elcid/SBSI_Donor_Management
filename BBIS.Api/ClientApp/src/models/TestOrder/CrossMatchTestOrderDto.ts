export interface CrossMatchTestOrderDto {
    CrossMatchTestOrderId: Guid;
    BloodComponentId: Guid;
    DonorTransactionId: Guid;
    BloodComponentName: string;
    DonorUnitSerialNumber: string;
    CrossMatchType: string;
    Result: string;
    Source: string;
    LISS_AGH: string;
    CollectionDate: Date;
    ExpiryDate: Date;
}