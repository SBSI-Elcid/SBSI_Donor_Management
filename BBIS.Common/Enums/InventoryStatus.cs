namespace BBIS.Common.Enums
{
    public static class InventoryStatus
    {
        public static readonly string InStock = nameof(InStock);
        public static readonly string Reserved = nameof(Reserved);
        public static readonly string NearExpiry = nameof(NearExpiry);
        public static readonly string Expired = nameof(Expired);
        public static readonly string Acquired = nameof(Acquired);
    }
}
