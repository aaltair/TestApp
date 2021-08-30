using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
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
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ProfileImg = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdateOn = table.Column<DateTime>(nullable: true),
                    Translate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookupType",
                columns: table => new
                {
                    LookupTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookupTypeName = table.Column<string>(nullable: true),
                    LookupTypeDescription = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdateOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Translate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupType", x => x.LookupTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LookupItem",
                columns: table => new
                {
                    LookupItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookupItemName = table.Column<string>(nullable: true),
                    LookupItemCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentLookupItemId = table.Column<int>(nullable: true),
                    LookupTypeId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: true),
                    Translate = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdateOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupItem", x => x.LookupItemId);
                    table.ForeignKey(
                        name: "FK_LookupItem_LookupType_LookupTypeId",
                        column: x => x.LookupTypeId,
                        principalTable: "LookupType",
                        principalColumn: "LookupTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LookupItem_LookupItem_ParentLookupItemId",
                        column: x => x.ParentLookupItemId,
                        principalTable: "LookupItem",
                        principalColumn: "LookupItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4643afee-eeb8-460c-8bf9-256613cb77c5", "ef4f3db5-6267-4a55-ba19-79016d7a33ae", "superadmin", "SUPERADMIN" },
                    { "c172130d-a149-412d-aa77-0b9986ebce6e", "1836d6d4-f607-4f40-9168-f88d9e9a295b", "admin", "ADMIN" },
                    { "52a4b656-2cec-4da1-ac36-d4468e9e1d25", "e6786c5c-b8ae-4bb6-b6d9-7827bd097e8d", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImg", "SecondName", "SecurityStamp", "Translate", "TwoFactorEnabled", "UpdateOn", "UpdatedBy", "UserName" },
                values: new object[] { "2e6bf32d-a0f3-4188-a1e2-5c2cfecffcae", 0, new DateTime(1993, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "f0f9a1aa-14a9-45aa-8009-a3f4459ee0d2", null, new DateTime(2021, 8, 30, 12, 30, 44, 740, DateTimeKind.Local).AddTicks(9407), "aaltair.developer@gmail.com", true, "Alaa", false, "Altair", false, null, "AALTAIR.DEVELOPER@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEHE6jAIIoTc523Fw4xT5kvdV80+dY3v0OW/czoJZaCcfmgbdJrp0mtC+ApKS/Ljb+A==", "+962788260020", true, null, "Abbas", "", @"{
  ""ar"": {
    ""FirstName"": ""Alaa1"",
    ""SecondName"": ""Abbas1"",
    ""LastName"": ""Altair1"",
    ""ProfileImg"": null,
    ""BirthDate"": ""1993-01-27T00:00:00"",
    ""IsDeleted"": false,
    ""CreatedBy"": null,
    ""CreatedOn"": ""2021-08-30T12:30:44.615797+03:00"",
    ""UpdatedBy"": null,
    ""UpdateOn"": null,
    ""Translate"": null,
    ""Id"": ""2e6bf32d-a0f3-4188-a1e2-5c2cfecffcae"",
    ""UserName"": ""admin"",
    ""NormalizedUserName"": ""ADMIN"",
    ""Email"": ""aaltair.developer@gmail.com"",
    ""NormalizedEmail"": ""AALTAIR.DEVELOPER@GMAIL.COM"",
    ""EmailConfirmed"": true,
    ""PasswordHash"": ""AQAAAAEAACcQAAAAEM4MvbeLFAKv44/jeo3waSxTt+2r6EtmWTneOrBQPPbSCRfNc6HhawVbfJYcxParwQ=="",
    ""SecurityStamp"": """",
    ""ConcurrencyStamp"": ""0bab02bd-1a8a-410a-b083-cd8cb61ffb4f"",
    ""PhoneNumber"": ""+962788260020"",
    ""PhoneNumberConfirmed"": true,
    ""TwoFactorEnabled"": false,
    ""LockoutEnd"": null,
    ""LockoutEnabled"": false,
    ""AccessFailedCount"": 0
  },
  ""en"": {
    ""FirstName"": ""Alaa"",
    ""SecondName"": ""Abbas"",
    ""LastName"": ""Altair"",
    ""ProfileImg"": null,
    ""BirthDate"": ""1993-01-27T00:00:00"",
    ""IsDeleted"": false,
    ""CreatedBy"": null,
    ""CreatedOn"": ""2021-08-30T12:30:44.7409407+03:00"",
    ""UpdatedBy"": null,
    ""UpdateOn"": null,
    ""Translate"": null,
    ""Id"": ""2e6bf32d-a0f3-4188-a1e2-5c2cfecffcae"",
    ""UserName"": ""admin"",
    ""NormalizedUserName"": ""ADMIN"",
    ""Email"": ""aaltair.developer@gmail.com"",
    ""NormalizedEmail"": ""AALTAIR.DEVELOPER@GMAIL.COM"",
    ""EmailConfirmed"": true,
    ""PasswordHash"": ""AQAAAAEAACcQAAAAEHE6jAIIoTc523Fw4xT5kvdV80+dY3v0OW/czoJZaCcfmgbdJrp0mtC+ApKS/Ljb+A=="",
    ""SecurityStamp"": """",
    ""ConcurrencyStamp"": ""f0f9a1aa-14a9-45aa-8009-a3f4459ee0d2"",
    ""PhoneNumber"": ""+962788260020"",
    ""PhoneNumberConfirmed"": true,
    ""TwoFactorEnabled"": false,
    ""LockoutEnd"": null,
    ""LockoutEnabled"": false,
    ""AccessFailedCount"": 0
  }
}", false, null, null, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2e6bf32d-a0f3-4188-a1e2-5c2cfecffcae", "4643afee-eeb8-460c-8bf9-256613cb77c5" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItem_LookupTypeId",
                table: "LookupItem",
                column: "LookupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItem_ParentLookupItemId",
                table: "LookupItem",
                column: "ParentLookupItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LookupItem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LookupType");
        }
    }
}
