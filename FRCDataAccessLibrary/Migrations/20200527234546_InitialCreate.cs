using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRCDataAccessLibrary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    EnglishName = table.Column<string>(maxLength: 50, nullable: false),
                    LanguageCode = table.Column<string>(maxLength: 20, nullable: false),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mods",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModTitles",
                schema: "public",
                columns: table => new
                {
                    ModId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModTitles", x => new { x.ModId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ModTitles_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "public",
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModTitles_Mods_ModId",
                        column: x => x.ModId,
                        principalSchema: "public",
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Releases",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ModId = table.Column<Guid>(nullable: false),
                    Version = table.Column<string>(maxLength: 50, nullable: false),
                    AddDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Releases_Mods_ModId",
                        column: x => x.ModId,
                        principalSchema: "public",
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModTitles_LanguageId",
                schema: "public",
                table: "ModTitles",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_ModId",
                schema: "public",
                table: "Releases",
                column: "ModId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModTitles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Releases",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Mods",
                schema: "public");
        }
    }
}
