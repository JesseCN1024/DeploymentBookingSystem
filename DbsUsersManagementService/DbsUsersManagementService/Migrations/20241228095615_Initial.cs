using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbsUsersManagementService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "identity",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DisplayName", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1fbd0afe-4e8c-4b9a-8631-735107d30cb2"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5164), "Power User", false, "POWER_USER", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5164) },
                    { new Guid("37d00899-66a7-4ed2-b8da-6ee0f8395201"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5166), "General User", false, "GENERAL_USER", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5167) },
                    { new Guid("e4b257ef-3638-4156-ad81-c98692b06229"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5160), "Admin", false, "ADMIN", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5160) }
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Teams",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5067), false, "Mocha", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5069) },
                    { new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5100), false, "Latte", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 28, 9, 56, 15, 416, DateTimeKind.Utc).AddTicks(5100) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "identity",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                schema: "identity",
                table: "Users",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "identity");
        }
    }
}
