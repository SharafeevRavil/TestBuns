using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Buns.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BunTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BunTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BunTypeId = table.Column<long>(type: "INTEGER", nullable: true),
                    TimeToSell = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    StartPrice = table.Column<double>(type: "REAL", nullable: false),
                    ControlSaleTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    BakeTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buns_BunTypes_BunTypeId",
                        column: x => x.BunTypeId,
                        principalTable: "BunTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buns_BunTypeId",
                table: "Buns",
                column: "BunTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buns");

            migrationBuilder.DropTable(
                name: "BunTypes");
        }
    }
}
