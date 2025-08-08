using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Warehouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputResources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    InputDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ResourceId = table.Column<long>(type: "bigint", nullable: false),
                    MeasurementUnitId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputResources_InputDocuments_InputDocumentId",
                        column: x => x.InputDocumentId,
                        principalTable: "InputDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InputResources_MeasurementUnits_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalTable: "MeasurementUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InputResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InputDocuments",
                columns: new[] { "Id", "CreatedAt", "Date", "Number", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "#001", null },
                    { 2L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "#002", null },
                    { 3L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), "#003", null },
                    { 4L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), "#004", null },
                    { 5L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), "#005", null }
                });

            migrationBuilder.InsertData(
                table: "MeasurementUnits",
                columns: new[] { "Id", "CreatedAt", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "тонна", 0, null },
                    { 2L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "шт", 0, null },
                    { 3L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "литр", 0, null },
                    { 4L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "кг", 0, null },
                    { 5L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "гр", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreatedAt", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Вода", 0, null },
                    { 2L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Картошка", 0, null },
                    { 3L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Зерно", 0, null },
                    { 4L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Nvidia GeForce RTX 5070 Ti", 0, null },
                    { 5L, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Монитор", 1, null }
                });

            migrationBuilder.InsertData(
                table: "InputResources",
                columns: new[] { "Id", "Amount", "CreatedAt", "InputDocumentId", "MeasurementUnitId", "ResourceId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 10m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 2L, 2L, 5L, null },
                    { 2L, 500m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1L, 3L, 1L, null },
                    { 3L, 800m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1L, 4L, 2L, null },
                    { 4L, 5m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1L, 1L, 3L, null },
                    { 5L, 7m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 3L, 2L, 4L, null },
                    { 6L, 250m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 4L, 3L, 1L, null },
                    { 7L, 15m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 5L, 2L, 4L, null },
                    { 8L, 15m, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), 5L, 2L, 5L, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputResources_InputDocumentId",
                table: "InputResources",
                column: "InputDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InputResources_MeasurementUnitId",
                table: "InputResources",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InputResources_ResourceId",
                table: "InputResources",
                column: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputResources");

            migrationBuilder.DropTable(
                name: "InputDocuments");

            migrationBuilder.DropTable(
                name: "MeasurementUnits");

            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
