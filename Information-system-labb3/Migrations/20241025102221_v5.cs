using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Information_system_labb3.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleEmployee",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleEmployeesEmployeeId",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_ResponsibleEmployeesEmployeeId",
                table: "Drivers",
                column: "ResponsibleEmployeesEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Employees_ResponsibleEmployeesEmployeeId",
                table: "Drivers",
                column: "ResponsibleEmployeesEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Employees_ResponsibleEmployeesEmployeeId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_ResponsibleEmployeesEmployeeId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "ResponsibleEmployeesEmployeeId",
                table: "Drivers");

            migrationBuilder.AlterColumn<string>(
                name: "ResponsibleEmployee",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
