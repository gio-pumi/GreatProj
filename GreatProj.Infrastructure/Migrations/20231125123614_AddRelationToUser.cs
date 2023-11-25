using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreatProj.Infrastructure.Migrations
{
    public partial class AddRelationToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "Clients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId2",
                table: "Clients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId1",
                table: "Clients",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId2",
                table: "Clients",
                column: "UserId2",
                unique: true,
                filter: "[UserId2] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_UserId1",
                table: "Clients",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_UserId2",
                table: "Clients",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_UserId1",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_UserId2",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId1",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId2",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Clients");
        }
    }
}
