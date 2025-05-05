namespace BBIS.Common.Enums
{
    public static class RequisitionStatus
    {
        public static readonly string Received = nameof(Received);
        public static readonly string InProgress = "In Progress";
        public static readonly string Done = nameof(Done);
        public static readonly string ForTransfusion = "For Transfusion";
        public static readonly string Cancelled = nameof(Cancelled);
    }
}
