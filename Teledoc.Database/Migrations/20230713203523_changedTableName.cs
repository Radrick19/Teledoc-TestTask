using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teledoc.Database.Migrations
{
    public partial class changedTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientsIncorporators_Client_ClientId",
                table: "ClientsIncorporators");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientsIncorporators_Founders_FounderId",
                table: "ClientsIncorporators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientsIncorporators",
                table: "ClientsIncorporators");

            migrationBuilder.RenameTable(
                name: "ClientsIncorporators",
                newName: "ClientFounders");

            migrationBuilder.RenameIndex(
                name: "IX_ClientsIncorporators_ClientId",
                table: "ClientFounders",
                newName: "IX_ClientFounders_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientFounders",
                table: "ClientFounders",
                columns: new[] { "FounderId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientFounders_Client_ClientId",
                table: "ClientFounders",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientFounders_Founders_FounderId",
                table: "ClientFounders",
                column: "FounderId",
                principalTable: "Founders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientFounders_Client_ClientId",
                table: "ClientFounders");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientFounders_Founders_FounderId",
                table: "ClientFounders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientFounders",
                table: "ClientFounders");

            migrationBuilder.RenameTable(
                name: "ClientFounders",
                newName: "ClientsIncorporators");

            migrationBuilder.RenameIndex(
                name: "IX_ClientFounders_ClientId",
                table: "ClientsIncorporators",
                newName: "IX_ClientsIncorporators_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientsIncorporators",
                table: "ClientsIncorporators",
                columns: new[] { "FounderId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsIncorporators_Client_ClientId",
                table: "ClientsIncorporators",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsIncorporators_Founders_FounderId",
                table: "ClientsIncorporators",
                column: "FounderId",
                principalTable: "Founders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
