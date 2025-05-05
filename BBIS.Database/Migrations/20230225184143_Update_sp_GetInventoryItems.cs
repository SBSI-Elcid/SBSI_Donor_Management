using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Update_sp_GetInventoryItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				DROP procedure IF EXISTS sp_GetInventoryItems;

				CREATE PROCEDURE sp_GetInventoryItems(IN status_list VARCHAR(200), IN bloodtype_list VARCHAR(200), IN datefilter_type varchar(15), IN date_from varchar(20), IN date_to varchar(20))
				BEGIN


					CREATE TEMPORARY TABLE ttSelectedBloodType (bloodType varchar(10), bloodRh varchar(10)); 
						INSERT INTO ttSelectedBloodType
						SELECT SUBSTRING_INDEX(val, SUBSTRING(val, -1, 1), 1) as bloodType
							   ,CASE WHEN SUBSTRING(val, -1, 1)  = '+' 
								THEN 'Positive' 
								ELSE 'Negative' END as bloodRh 
						FROM
						(SELECT
						  DISTINCT SUBSTRING_INDEX(SUBSTRING_INDEX(bloodtype_list, ',', n.digit+1), ',', -1) val
						FROM
						  (SELECT bloodtype_list) as commaSeparatedList
						  INNER JOIN
						  (SELECT 0 digit UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3  UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6) n
						  ON LENGTH(REPLACE(bloodtype_list, ',' , '')) <= LENGTH(bloodtype_list)-n.digit) as bloodTypes;


					SELECT 
						ii.InventoryItemId,
						ii.BloodComponentId,
						bc.ComponentName,
						ii.BloodType,
						ii.BloodRh,
						ii.UnitSerialNumber,
						concat(ii.Volume,ii.UnitMeasure) AS NoOfUnit,
						dbc.StartTime AS CollectionDate,
						(case when isrc.InstitutionId is not null 
						 THEN ins.Name 
						 ELSE concat(ua.FirstName, ' ', ua.LastName)
						 END)  as CollectedBy,
						ii.ExpiryDate,
						ii.Status
					FROM inventoryitem AS ii
						INNER JOIN ttSelectedBloodType as tt on ii.BloodType = tt.bloodType AND ii.BloodRh = tt.bloodRh
						INNER JOIN inventorysource AS isrc ON ii.InventorySourceId = isrc.InventorySourceId
						INNER JOIN bloodcomponent AS bc ON bc.BloodComponentId = ii.BloodComponentId
						LEFT JOIN institution AS ins ON ins.InstitutionId = isrc.InstitutionId
						LEFT JOIN donorbloodcollection AS dbc ON dbc.DonorTransactionId = isrc.DonorTranctionId
						LEFT JOIN useraccount AS ua ON ua.UserAccountId = dbc.FacilitatedById
					WHERE 
						(status_list = '' OR FIND_IN_SET(ii.Status, status_list))
						AND
						(datefilter_type = '' OR
						 (datefilter_type = 'expiry' AND date(ii.ExpiryDate) between date_from and date_to)
						  OR
						 (datefilter_type = 'collection' AND date(dbc.StartTime) between date_from and date_to)
						);
				END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				DROP procedure IF EXISTS sp_GetInventoryItems;

				CREATE PROCEDURE sp_GetInventoryItems(IN status_list VARCHAR(200), IN bloodtype_list VARCHAR(200), IN datefilter_type varchar(15), IN date_from varchar(20), IN date_to varchar(20))
				BEGIN
					SELECT 
						ii.InventoryItemId,
						ii.BloodComponentId,
						bc.ComponentName,
						ii.BloodType,
						ii.UnitSerialNumber,
						concat(ii.Volume,ii.UnitMeasure) AS NoOfUnit,
						dbc.StartTime AS CollectionDate,
						(case when isrc.InstitutionId is not null 
						 THEN ins.Name 
						 ELSE concat(ua.FirstName, ' ', ua.LastName)
						 END)  as CollectedBy,
						ii.ExpiryDate,
						ii.Status
					FROM inventoryitem AS ii
						INNER JOIN inventorysource AS isrc ON ii.InventorySourceId = isrc.InventorySourceId
						INNER JOIN bloodcomponent AS bc ON bc.BloodComponentId = ii.BloodComponentId
						LEFT JOIN institution AS ins ON ins.InstitutionId = isrc.InstitutionId
						LEFT JOIN donorbloodcollection AS dbc ON dbc.DonorTransactionId = isrc.DonorTranctionId
						LEFT JOIN useraccount AS ua ON ua.UserAccountId = dbc.FacilitatedById
					WHERE 
						(status_list = '' OR FIND_IN_SET(ii.Status, status_list))
						AND
						(bloodtype_list = '' OR FIND_IN_SET(ii.BloodType, bloodtype_list))
						AND
						(datefilter_type = '' OR
						 (datefilter_type = 'expiry' AND date(ii.ExpiryDate) between date_from and date_to)
						  OR
						 (datefilter_type = 'collection' AND date(dbc.StartTime) between date_from and date_to)
						);
				END
            ");
        }
    }
}
