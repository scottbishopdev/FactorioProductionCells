using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FactorioProductionCells.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "DependencyComparisonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyComparisonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DependencyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependencyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    EnglishName = table.Column<string>(maxLength: 100, nullable: false),
                    LanguageTag = table.Column<string>(maxLength: 20, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    PreferredLanguageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Languages_PreferredLanguageId",
                        column: x => x.PreferredLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    AddedBy = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<Guid>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mods_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mods_Users_LastModifiedBy",
                        column: x => x.LastModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModTitles",
                columns: table => new
                {
                    ModId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    AddedBy = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<Guid>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModTitles", x => new { x.ModId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ModTitles_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModTitles_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModTitles_Users_LastModifiedBy",
                        column: x => x.LastModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModTitles_Mods_ModId",
                        column: x => x.ModId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Releases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    AddedBy = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<Guid>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    ModId = table.Column<Guid>(nullable: false),
                    ReleasedAt = table.Column<DateTime>(nullable: false),
                    Sha1 = table.Column<string>(maxLength: 50, nullable: false),
                    Version_Major = table.Column<int>(nullable: true),
                    Version_Minor = table.Column<int>(nullable: true),
                    Version_Patch = table.Column<int>(nullable: true),
                    FactorioVersion_Major = table.Column<int>(nullable: true),
                    FactorioVersion_Minor = table.Column<int>(nullable: true),
                    DownloadUrl_ModName = table.Column<string>(maxLength: 200, nullable: true),
                    DownloadUrl_ReleaseToken = table.Column<string>(maxLength: 24, nullable: true),
                    ReleaseFileName_ModName = table.Column<string>(maxLength: 200, nullable: true),
                    ReleaseFileName_Version_Major = table.Column<int>(nullable: true),
                    ReleaseFileName_Version_Minor = table.Column<int>(nullable: true),
                    ReleaseFileName_Version_Patch = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Releases_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Releases_Users_LastModifiedBy",
                        column: x => x.LastModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Releases_Mods_ModId",
                        column: x => x.ModId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependencies",
                columns: table => new
                {
                    ReleaseId = table.Column<Guid>(nullable: false),
                    DependentModId = table.Column<Guid>(nullable: false),
                    AddedBy = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<Guid>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    DependencyTypeId = table.Column<int>(nullable: false),
                    DependentModName = table.Column<string>(maxLength: 200, nullable: false),
                    DependencyComparisonTypeId = table.Column<int>(nullable: false),
                    DependentModVersion_Major = table.Column<int>(nullable: true),
                    DependentModVersion_Minor = table.Column<int>(nullable: true),
                    DependentModVersion_Patch = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependencies", x => new { x.ReleaseId, x.DependentModId });
                    table.ForeignKey(
                        name: "FK_Dependencies_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependencies_DependencyComparisonTypes_DependencyComparison~",
                        column: x => x.DependencyComparisonTypeId,
                        principalTable: "DependencyComparisonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependencies_DependencyTypes_DependencyTypeId",
                        column: x => x.DependencyTypeId,
                        principalTable: "DependencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependencies_Mods_DependentModId",
                        column: x => x.DependentModId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependencies_Users_LastModifiedBy",
                        column: x => x.LastModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dependencies_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DependencyComparisonTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "LessThan" },
                    { 1, "LessThanOrEqualTo" },
                    { 2, "EqualTo" },
                    { 3, "GreaterThan" },
                    { 4, "GreaterThanOrEqualTo" }
                });

            migrationBuilder.InsertData(
                table: "DependencyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Required" },
                    { 1, "Incompatibility" },
                    { 2, "Optional" },
                    { 3, "HiddenOptional" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependencies_AddedBy",
                table: "Dependencies",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencies_DependencyComparisonTypeId",
                table: "Dependencies",
                column: "DependencyComparisonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencies_DependencyTypeId",
                table: "Dependencies",
                column: "DependencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencies_DependentModId",
                table: "Dependencies",
                column: "DependentModId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependencies_LastModifiedBy",
                table: "Dependencies",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_IsDefault",
                table: "Languages",
                column: "IsDefault",
                unique: true,
                filter: "\"IsDefault\" = true");

            migrationBuilder.CreateIndex(
                name: "IX_Mods_AddedBy",
                table: "Mods",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Mods_LastModifiedBy",
                table: "Mods",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Mods_Name",
                table: "Mods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModTitles_AddedBy",
                table: "ModTitles",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModTitles_LanguageId",
                table: "ModTitles",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ModTitles_LastModifiedBy",
                table: "ModTitles",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_AddedBy",
                table: "Releases",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_LastModifiedBy",
                table: "Releases",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_ModId",
                table: "Releases",
                column: "ModId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PreferredLanguageId",
                table: "Users",
                column: "PreferredLanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependencies");

            migrationBuilder.DropTable(
                name: "ModTitles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "DependencyComparisonTypes");

            migrationBuilder.DropTable(
                name: "DependencyTypes");

            migrationBuilder.DropTable(
                name: "Releases");

            migrationBuilder.DropTable(
                name: "Mods");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
