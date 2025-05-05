using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBIS.Database.Migrations
{
    public partial class Add_sp_GetDonorReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetDonorReport(IN status_list VARCHAR(200), IN bloodtype_list VARCHAR(200), IN date_from varchar(20), IN date_to varchar(20))
                BEGIN
                    SELECT 
	                    dt.UnitSerialNumber,
	                    d.Gender,
	                    TIMESTAMPDIFF(YEAR, d.BirthDate, CURDATE()) AS Age,
                        IFNULL(dt.FinalBloodType, dis.BloodType) AS BloodType,
                        IFNULL(dt.BloodRh, 'Unknown') AS BloodRh,
                        IFNULL(CONCAT(dbc.CollectedBloodAmount, ' ', dbc.UnitOfMeasurement), 'N/A') AS CollectedAmount,
                        dt.DateOfDonation,
                        CASE 
		                    WHEN dt.DonorStatus = 'Deferred' THEN 'Deferred'
		                    WHEN dt.DonorStatus = 'Success' OR dt.DonorStatus = 'Inventory' THEN 'Success'
                            ELSE 'Ongoing'
	                    END AS Status
                    FROM donortransaction AS dt
                    INNER JOIN donor AS d on dt.DonorId = d.DonorId
                    LEFT JOIN donorbloodcollection AS dbc on dt.DonorTransactionId = dbc.DonorTransactionId
                    LEFT JOIN donorinitialscreening AS dis on dt.DonorTransactionId = dis.DonorTransactionId
	                WHERE 
		                dt.DateOfDonation IS NOT NULL
                        AND (status_list = '' OR FIND_IN_SET(dt.DonorStatus, status_list))
                        AND (bloodtype_list = '' OR FIND_IN_SET(dt.FinalBloodType, bloodtype_list) OR FIND_IN_SET(dis.BloodType, bloodtype_list))
		                AND (date_from IS NULL OR date_to IS NULL OR date(dt.DateOfDonation) between date_from and date_to);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
