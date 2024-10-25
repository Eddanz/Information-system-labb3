using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Information_system_labb3.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleEmployee",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleEmployeeId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleEmployeeId",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleEmployee",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
