using BBIS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBIS.Database.MigrationHelper
{
    public static class LookupDataSeed
    {
        public static void Setup(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lookup>().HasData(
                new Lookup
                {   LookupId = 1, IsActive = true,  LookupKey = "lookup.BloodTypes", Description = "Blood type options",
                },
                new Lookup
                {
                    LookupId = 2,
                    IsActive = true,
                    LookupKey = "lookup.DonationTypes",
                    Description = "Blood Donation Type options",
                },
                new Lookup
                {
                    LookupId = 3,
                    IsActive = true,
                    LookupKey = "lookup.InHouseTypes",
                    Description = "In house type options",
                },
                new Lookup
                {
                    LookupId = 4,
                    IsActive = true,
                    LookupKey = "lookup.BloodBagTypes",
                    Description = "Blood bag type options",
                },
                new Lookup
                {
                    LookupId = 5,
                    IsActive = true,
                    LookupKey = "lookup.AgencyTypes",
                    Description = "Agency type options",
                },
                new Lookup
                {
                    LookupId = 6,
                    IsActive = true,
                    LookupKey = "lookup.DonorStatus",
                    Description = "Donor Status options",
                },
                new Lookup
                {
                    LookupId = 7,
                    IsActive = true,
                    LookupKey = "lookup.BloodBagSizeTypes",
                    Description = "Blood Bag Size options",
                },
                new Lookup
                {
                    LookupId = 8,
                    IsActive = true,
                    LookupKey = "lookup.SpecialBagTypes",
                    Description = "Special Bag type options",
                },
                new Lookup
                {
                    LookupId = 9,
                    IsActive = true,
                    LookupKey = "lookup.ApheresisBagTypes",
                    Description = "Apheresis Bag type options",
                },
                new Lookup
                {
                    LookupId = 10,
                    IsActive = true,
                    LookupKey = "lookup.BloodBagCollectionTypes",
                    Description = "Blood Bag Collection Type options",
                },
                new Lookup
                {
                    LookupId = 11,
                    IsActive = true,
                    LookupKey = "lookup.CivilStatus",
                    Description = "Civil Status options",
                },
                new Lookup
                {
                    LookupId = 12,
                    IsActive = true,
                    LookupKey = "lookup.DeferralStatus",
                    Description = "Deferral Status options",
                },
                new Lookup
                {
                    LookupId = 13,
                    IsActive = true,
                    LookupKey = "lookup.PhysicalExamResult",
                    Description = "Physical Exam Result options",
                },
                new Lookup
                {
                    LookupId = 14,
                    IsActive = true,
                    LookupKey = "lookup.PriorityLevel",
                    Description = "Priority Level options",
                },
                new Lookup
                {
                    LookupId = 15,
                    IsActive = true,
                    LookupKey = "lookup.CrossMatchingOptions",
                    Description = "Cross Matching options",
                },
                new Lookup
                {
                    LookupId = 16,
                    IsActive = true,
                    LookupKey = "lookup.PatientType",
                    Description = "Patient Type options",
                },
                new Lookup
                {
                    LookupId = 17,
                    IsActive = true,
                    LookupKey = "lookup.Membership",
                    Description = "Priority Level options",
                },
                new Lookup
                {
                    LookupId = 18,
                    IsActive = true,
                    LookupKey = "lookup.CrossMatchTypes",
                    Description = "Cross Match Types",
                }
           );
        }

        public static void SetupLookupOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LookupOption>().HasData(
                new LookupOption { LookupId = 1, LookupOptionId = 1, Name = "O", Value = "O", IsActive = true },
                new LookupOption { LookupId = 1, LookupOptionId = 2, Name = "A", Value = "A", IsActive = true },
                new LookupOption { LookupId = 1, LookupOptionId = 3, Name = "B", Value = "B", IsActive = true },
                new LookupOption { LookupId = 1, LookupOptionId = 4, Name = "AB", Value = "AB", IsActive = true },

                new LookupOption { LookupId = 2, LookupOptionId = 5, Name = "In-House", Value = "InHouse", IsActive = true },
                new LookupOption { LookupId = 2, LookupOptionId = 6, Name = "Mobile Blood Donation", Value = "Mobile", IsActive = true },

                new LookupOption { LookupId = 3, LookupOptionId = 7, Name = "WALK-IN/VOLUNTARY", Value = "WalkInOrVolountary", IsActive = true },
                new LookupOption { LookupId = 3, LookupOptionId = 8, Name = "REPLACEMENT", Value = "Replacement", IsActive = true },
                new LookupOption { LookupId = 3, LookupOptionId = 9, Name = "PATIENT-DIRECTED", Value = "PatientDirected", IsActive = true },

                new LookupOption { LookupId = 4, LookupOptionId = 10, Name = "Single", Value = "Single", IsActive = true },
                new LookupOption { LookupId = 4, LookupOptionId = 11, Name = "Multiple", Value = "Multiple", IsActive = true },
                new LookupOption { LookupId = 4, LookupOptionId = 12, Name = "Top & Bottom", Value = "TopAndBottom", IsActive = true },
                new LookupOption { LookupId = 4, LookupOptionId = 13, Name = "Apheresis", Value = "Apheresis", IsActive = true },

                new LookupOption { LookupId = 5, LookupOptionId = 14, Name = "Red Cross", Value = "RedCross", IsActive = true },
                new LookupOption { LookupId = 5, LookupOptionId = 15, Name = "Hospital", Value = "Hospital", IsActive = true },

                new LookupOption { LookupId = 6, LookupOptionId = 16, Name = "For Initial Screening", Value = "ForInitialScreening", IsActive = true },
                new LookupOption { LookupId = 6, LookupOptionId = 17, Name = "For Physical Examination", Value = "ForPhysicalExamination", IsActive = true },
                new LookupOption { LookupId = 6, LookupOptionId = 18, Name = "For Blood Collection", Value = "ForBloodCollection", IsActive = true },
                new LookupOption { LookupId = 6, LookupOptionId = 19, Name = "For Laboratory Test", Value = "ForLaboratoryTest", IsActive = true },
                new LookupOption { LookupId = 6, LookupOptionId = 20, Name = "Success", Value = "Success", IsActive = true },
                new LookupOption { LookupId = 6, LookupOptionId = 21, Name = "Deferred", Value = "Deferred", IsActive = true },

                new LookupOption { LookupId = 7, LookupOptionId = 22, Name = "Single", Value = "Single", IsActive = true },
                new LookupOption { LookupId = 7, LookupOptionId = 23, Name = "Double", Value = "Double", IsActive = true },
                new LookupOption { LookupId = 7, LookupOptionId = 24, Name = "Tripple", Value = "Tripple", IsActive = true },
                new LookupOption { LookupId = 7, LookupOptionId = 25, Name = "Quadruple", Value = "Quadruple", IsActive = true },

                new LookupOption { LookupId = 8, LookupOptionId = 26, Name = "FK T&B", Value = "FK T&B", IsActive = true },
                new LookupOption { LookupId = 8, LookupOptionId = 27, Name = "TRM T&B", Value = "TRM T&B", IsActive = true },

                new LookupOption { LookupId = 9, LookupOptionId = 28, Name = "FRES", Value = "FRES", IsActive = true },
                new LookupOption { LookupId = 9, LookupOptionId = 29, Name = "AMI", Value = "AMI", IsActive = true },
                new LookupOption { LookupId = 9, LookupOptionId = 30, Name = "HAE", Value = "HAE", IsActive = true },
                new LookupOption { LookupId = 9, LookupOptionId = 31, Name = "TRI", Value = "TRI", IsActive = true },

                new LookupOption { LookupId = 10, LookupOptionId = 32, Name = "KARMI", Value = "KARMI", IsActive = true },
                new LookupOption { LookupId = 10, LookupOptionId = 33, Name = "TERUMO", Value = "TERUMO", IsActive = true },
                new LookupOption { LookupId = 10, LookupOptionId = 34, Name = "SPECIAL BAG", Value = "SpecialBag", IsActive = true },
                new LookupOption { LookupId = 10, LookupOptionId = 35, Name = "APHERESIS", Value = "Apheresis", IsActive = true },

                new LookupOption { LookupId = 11, LookupOptionId = 36, Name = "Single", Value = "Single", IsActive = true },
                new LookupOption { LookupId = 11, LookupOptionId = 37, Name = "Married", Value = "Married", IsActive = true },
                new LookupOption { LookupId = 11, LookupOptionId = 38, Name = "Separated", Value = "Separated", IsActive = true },
                new LookupOption { LookupId = 11, LookupOptionId = 39, Name = "Widow", Value = "Widow", IsActive = true },

                new LookupOption { LookupId = 12, LookupOptionId = 40, Name = "Permanent", Value = "Permanent", IsActive = true },
                new LookupOption { LookupId = 12, LookupOptionId = 41, Name = "Temporary", Value = "Temporary", IsActive = true },

                new LookupOption { LookupId = 13, LookupOptionId = 42, Name = "Passed", Value = "Passed", IsActive = true },
                new LookupOption { LookupId = 13, LookupOptionId = 43, Name = "Temporary Deferral", Value = "TemporaryDeferral", IsActive = true },
                new LookupOption { LookupId = 13, LookupOptionId = 44, Name = "Permanent Deferral", Value = "PermanentDeferral", IsActive = true },
                new LookupOption { LookupId = 13, LookupOptionId = 45, Name = "Refused", Value = "Refused", IsActive = true },

                new LookupOption { LookupId = 14, LookupOptionId = 46, Name = "Routine", Value = "Routine", IsActive = true },
                new LookupOption { LookupId = 14, LookupOptionId = 47, Name = "STAT", Value = "STAT", IsActive = true },
                new LookupOption { LookupId = 14, LookupOptionId = 48, Name = "Schedule", Value = "Schedule", IsActive = true },
                
                new LookupOption { LookupId = 15, LookupOptionId = 49, Name = "Saline Phase Only", Value = "SalinePhaseOnly", IsActive = true },
                new LookupOption { LookupId = 15, LookupOptionId = 50, Name = "Saline Albumin Phase Only", Value = "SalineAlbuminPhaseOnly", IsActive = true },
                new LookupOption { LookupId = 15, LookupOptionId = 51, Name = "Saline Albumin Globulin Phase Only", Value = "SalineAlbuminGlobulinPhaseOnly", IsActive = true },

                new LookupOption { LookupId = 16, LookupOptionId = 52, Name = "Out Patient", Value = "OutPatient", IsActive = true },
                new LookupOption { LookupId = 16, LookupOptionId = 53, Name = "In Patient", Value = "InPatient", IsActive = true },

                new LookupOption { LookupId = 17, LookupOptionId = 54, Name = "GSIS", Value = "GSIS", IsActive = true },
                new LookupOption { LookupId = 17, LookupOptionId = 55, Name = "SSS", Value = "SSS", IsActive = true },
                new LookupOption { LookupId = 17, LookupOptionId = 56, Name = "OCW", Value = "OCW", IsActive = true },
                new LookupOption { LookupId = 17, LookupOptionId = 57, Name = "Dependent", Value = "Dependent", IsActive = true },
                new LookupOption { LookupId = 17, LookupOptionId = 58, Name = "Indigent", Value = "Indigent", IsActive = true },

                new LookupOption { LookupId = 18, LookupOptionId = 59, Name = "Major", Value = "Major", IsActive = true },
                new LookupOption { LookupId = 18, LookupOptionId = 60, Name = "Minor", Value = "Minor", IsActive = true },
                new LookupOption { LookupId = 18, LookupOptionId = 61, Name = "Neutral", Value = "Neutral", IsActive = true },
                new LookupOption { LookupId = 18, LookupOptionId = 62, Name = "AHG", Value = "AHG", IsActive = true },

                new LookupOption { LookupId = 6, LookupOptionId = 63, Name = "Inventory", Value = "Inventory", IsActive = true },
                new LookupOption { LookupId = 17, LookupOptionId = 64, Name = "N/A", Value = "N/A", IsActive = true }
          );
        }
    }
}
