using Microsoft.EntityFrameworkCore.Migrations;

namespace TetrioStats.Data.Migrations.CoreMYSQLContext
{
	public partial class CoreMYSQLContext00 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
					name: "Username",
					table: "TLStatsEntries",
					maxLength: 16,
					nullable: false,
					oldClrType: typeof(string),
					oldType: "longtext CHARACTER SET utf8mb4",
					oldNullable: true);

			migrationBuilder.AlterColumn<string>(
					name: "UserRank",
					table: "TLStatsEntries",
					fixedLength: true,
					maxLength: 2,
					nullable: false,
					oldClrType: typeof(string),
					oldType: "longtext CHARACTER SET utf8mb4");

			migrationBuilder.AlterColumn<string>(
					name: "UserID",
					table: "TLStatsEntries",
					fixedLength: true,
					maxLength: 24,
					nullable: false,
					oldClrType: typeof(string),
					oldType: "varchar(255) CHARACTER SET utf8mb4",
					oldNullable: true);

			migrationBuilder.AlterColumn<string>(
					name: "Country",
					table: "TLStatsEntries",
					fixedLength: true,
					maxLength: 2,
					nullable: false,
					oldClrType: typeof(string),
					oldType: "longtext CHARACTER SET utf8mb4");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
					name: "Username",
					table: "TLStatsEntries",
					type: "longtext CHARACTER SET utf8mb4",
					nullable: true,
					oldClrType: typeof(string),
					oldMaxLength: 16);

			migrationBuilder.AlterColumn<string>(
					name: "UserRank",
					table: "TLStatsEntries",
					type: "longtext CHARACTER SET utf8mb4",
					nullable: false,
					oldClrType: typeof(string),
					oldFixedLength: true,
					oldMaxLength: 2);

			migrationBuilder.AlterColumn<string>(
					name: "UserID",
					table: "TLStatsEntries",
					type: "varchar(255) CHARACTER SET utf8mb4",
					nullable: true,
					oldClrType: typeof(string),
					oldFixedLength: true,
					oldMaxLength: 24);

			migrationBuilder.AlterColumn<string>(
					name: "Country",
					table: "TLStatsEntries",
					type: "longtext CHARACTER SET utf8mb4",
					nullable: false,
					oldClrType: typeof(string),
					oldFixedLength: true,
					oldMaxLength: 2);
		}
	}
}
