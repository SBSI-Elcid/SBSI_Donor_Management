namespace BBIS.Common.Enums
{
    public static class ApplicationSettingKeys
    {
        // Expiry Value will be in days
        //public static readonly string PackedRedBloodCellsExpiry = nameof(PackedRedBloodCellsExpiry);
        //public static readonly string FreshFrozenPlasmaExpiry = nameof(FreshFrozenPlasmaExpiry);
        //public static readonly string PlateletConcentrateExpiry = nameof(PlateletConcentrateExpiry);
        //public static readonly string LeukoreducedRedBloodCellsExpiry = nameof(LeukoreducedRedBloodCellsExpiry);

        // institution or current company name
        public static readonly string InstitutionName = nameof(InstitutionName); 

        // mL, L, CC, UNIT
        public static readonly string BloodCollectionUnitOfMeasure = nameof(BloodCollectionUnitOfMeasure);
        public static readonly string BloodComponentsUnitOfMeasure = nameof(BloodComponentsUnitOfMeasure);
    }

}
