using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTrackingSystem.DataAccess.Migrations
{
    public partial class mappinguserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Vehicle_VehicleId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_User_UsersUserId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_UsersUserId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vehicle",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Location",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_UserId",
                table: "Vehicle",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Vehicle_VehicleId",
                table: "Location",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Vehicle_VehicleId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_UserId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Vehicle",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_UsersUserId",
                table: "Vehicle",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Vehicle_VehicleId",
                table: "Location",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_User_UsersUserId",
                table: "Vehicle",
                column: "UsersUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
