using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTrackingSystem.DataAccess.Migrations
{
    public partial class addeddevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_UsersId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Vehicles",
                newName: "UsersUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vehicles",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_UsersId",
                table: "Vehicles",
                newName: "IX_Vehicles_UsersUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserType",
                newName: "UserTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImeiNumber = table.Column<Guid>(maxLength: 16, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_VehicleId",
                table: "Devices",
                column: "VehicleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_UsersUserId",
                table: "Vehicles",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_UsersUserId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "Vehicles",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Vehicles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_UsersUserId",
                table: "Vehicles",
                newName: "IX_Vehicles_UsersId");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "UserType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_UsersId",
                table: "Vehicles",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
