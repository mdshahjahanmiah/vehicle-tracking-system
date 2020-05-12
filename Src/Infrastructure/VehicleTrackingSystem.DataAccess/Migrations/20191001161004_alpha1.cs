using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTrackingSystem.DataAccess.Migrations
{
    public partial class alpha1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "MobileNumber", "Name", "Password", "UserName", "UserTypeId" },
                values: new object[] { 1, "blackbee08@gmail.com", "0809201582", "Md Shahjahan Miah", "6jlpGIp6udJ6oiRYkphMlOf0WQW9l8gVPjFXjVhGKi/+nIiX", "Miah", 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "MobileNumber", "Name", "Password", "UserName", "UserTypeId" },
                values: new object[] { 2, "shahjahan@2c2p.com", "0809201583", "Hasan Shahjahan", "6jlpGIp6udJ6oiRYkphMlOf0WQW9l8gVPjFXjVhGKi/+nIiX", "Hasan", 2 });
        }
    }
}
