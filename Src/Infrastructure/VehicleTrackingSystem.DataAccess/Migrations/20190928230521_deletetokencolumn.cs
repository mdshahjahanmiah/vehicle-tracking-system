using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTrackingSystem.DataAccess.Migrations
{
    public partial class deletetokencolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "6jlpGIp6udJ6oiRYkphMlOf0WQW9l8gVPjFXjVhGKi/+nIiX");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "6jlpGIp6udJ6oiRYkphMlOf0WQW9l8gVPjFXjVhGKi/+nIiX");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "User",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "123456");
        }
    }
}
