using Microsoft.EntityFrameworkCore.Migrations;

namespace TetrioStats.Data.Migrations.CoreMYSQLContext
{
	public partial class CoreMYSQLContext01 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
					name: "UserRank",
					table: "TLStatsEntries",
					maxLength: 2,
					nullable: false,
					oldClrType: typeof(string),
					oldType: "char(2) CHARACTER SET utf8mb4",
					oldFixedLength: true,
					oldMaxLength: 2);

			migrationBuilder.AlterColumn<string>(
					name: "UserID",
					table: "TLStatsEntries",
					maxLength: 24,
					nullable: false,
					oldClrType: typeof(string),
					oldType: "char(24) CHARACTER SET utf8mb4",
					oldFixedLength: true,
					oldMaxLength: 24);

			migrationBuilder.AlterColumn<string>(
					name: "Country",
					table: "TLStatsEntries",
					maxLength: 2,
					nullable: false,
					oldClrType: typeof(string),
					oldType: "char(2) CHARACTER SET utf8mb4",
					oldFixedLength: true,
					oldMaxLength: 2);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
					name: "UserRank",
					table: "TLStatsEntries",
					type: "char(2) CHARACTER SET utf8mb4",
					fixedLength: true,
					maxLength: 2,
					nullable: false,
					oldClrType: typeof(string),
					oldMaxLength: 2);

			migrationBuilder.AlterColumn<string>(
					name: "UserID",
					table: "TLStatsEntries",
					type: "char(24) CHARACTER SET utf8mb4",
					fixedLength: true,
					maxLength: 24,
					nullable: false,
					oldClrType: typeof(string),
					oldMaxLength: 24);

			migrationBuilder.AlterColumn<string>(
					name: "Country",
					table: "TLStatsEntries",
					type: "char(2) CHARACTER SET utf8mb4",
					fixedLength: true,
					maxLength: 2,
					nullable: false,
					oldClrType: typeof(string),
					oldMaxLength: 2);
		}
	}
}
