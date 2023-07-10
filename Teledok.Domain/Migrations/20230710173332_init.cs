using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teledok.Domain.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incorporators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incorporators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncorporatorId = table.Column<int>(type: "int", nullable: false),
                    Inn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPersons_Incorporators_IncorporatorId",
                        column: x => x.IncorporatorId,
                        principalTable: "Incorporators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntitiesIncorporators",
                columns: table => new
                {
                    LegalEntityId = table.Column<int>(type: "int", nullable: false),
                    IncorporatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntitiesIncorporators", x => new { x.IncorporatorId, x.LegalEntityId });
                    table.ForeignKey(
                        name: "FK_LegalEntitiesIncorporators_Incorporators_IncorporatorId",
                        column: x => x.IncorporatorId,
                        principalTable: "Incorporators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LegalEntitiesIncorporators_LegalEntities_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalTable: "LegalEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPersons_IncorporatorId",
                table: "IndividualPersons",
                column: "IncorporatorId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntitiesIncorporators_LegalEntityId",
                table: "LegalEntitiesIncorporators",
                column: "LegalEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualPersons");

            migrationBuilder.DropTable(
                name: "LegalEntitiesIncorporators");

            migrationBuilder.DropTable(
                name: "Incorporators");

            migrationBuilder.DropTable(
                name: "LegalEntities");
        }
    }
}
