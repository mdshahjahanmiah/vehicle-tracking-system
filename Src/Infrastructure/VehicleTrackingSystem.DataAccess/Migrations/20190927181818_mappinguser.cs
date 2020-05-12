using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTrackingSystem.DataAccess.Migrations
{
    public partial class mappinguser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Vehicle",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Vehicle",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
