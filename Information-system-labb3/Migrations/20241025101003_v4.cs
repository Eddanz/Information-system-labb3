using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Information_system_labb3.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Employees_EmployeeId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_EmployeeId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "ResponsibleEmployeeId",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleEmployee",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleEmployee",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleEmployeeId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_EmployeeId",
                table: "Drivers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Employees_EmployeeId",
                table: "Drivers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
