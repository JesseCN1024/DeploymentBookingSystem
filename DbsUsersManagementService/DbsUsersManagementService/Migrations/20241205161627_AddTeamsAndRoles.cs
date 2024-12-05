using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbsUsersManagementService.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamsAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DisplayName", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1fbd0afe-4e8c-4b9a-8631-735107d30cb2"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5213), "Power User", false, "POWER_USER", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5214) },
                    { new Guid("37d00899-66a7-4ed2-b8da-6ee0f8395201"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5216), "General User", false, "GENERAL_USER", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5217) },
                    { new Guid("e4b257ef-3638-4156-ad81-c98692b06229"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5209), "Admin", false, "ADMIN", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5209) }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5123), false, "Mocha", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5133) },
                    { new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"), new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5137), false, "Latte", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5138) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
