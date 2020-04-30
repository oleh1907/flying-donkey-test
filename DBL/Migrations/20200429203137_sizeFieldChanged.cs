using Microsoft.EntityFrameworkCore.Migrations;

namespace DBL.Migrations
{
    public partial class sizeFieldChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SizeInKb",
                table: "Files",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SizeInMb",
                table: "Files",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
