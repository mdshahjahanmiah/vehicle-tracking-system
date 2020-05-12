using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTrackingSystem.DataAccess.Migrations
{
    public partial class changeddevicemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Vehicles_VehicleId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_VehicleId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Devices");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DeviceId",
                table: "Vehicles",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Devices_DeviceId",
                table: "Vehicles",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Devices_DeviceId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_DeviceId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Devices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_VehicleId",
                table: "Devices",
                column: "VehicleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Vehicles_VehicleId",
                table: "Devices",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
