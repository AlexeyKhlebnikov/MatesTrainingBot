using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "training");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "TrainingTypes",
                schema: "training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "training",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TelegramId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSubTypes",
                schema: "training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainingTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSubTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSubTypes_TrainingTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalSchema: "training",
                        principalTable: "TrainingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "training",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigserial", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubscriberId = table.Column<long>(type: "bigint", nullable: false),
                    location = table.Column<Point>(type: "geometry(Point,4326)", nullable: false),
                    radius = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: true),
                    SubTypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_TrainingSubTypes_SubTypeId",
                        column: x => x.SubTypeId,
                        principalSchema: "training",
                        principalTable: "TrainingSubTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_TrainingTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "training",
                        principalTable: "TrainingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalSchema: "training",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                schema: "training",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigserial", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    onDate = table.Column<DateOnly>(type: "date", nullable: false),
                    onTime = table.Column<DateTimeOffset>(type: "time with time zone", nullable: false),
                    TrainingTypeId = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    startPoint = table.Column<Point>(type: "geometry(Point,4326)", nullable: false),
                    distance = table.Column<int>(type: "integer", nullable: false),
                    averageSpeed = table.Column<double>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_TrainingSubTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalSchema: "training",
                        principalTable: "TrainingSubTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainings_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "training",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "training",
                table: "TrainingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Бег" },
                    { 2, "Велоспорт" },
                    { 3, "Беговые лыжи" },
                    { 4, "Плавание" }
                });

            migrationBuilder.InsertData(
                schema: "training",
                table: "TrainingSubTypes",
                columns: new[] { "Id", "Name", "TrainingTypeId" },
                values: new object[,]
                {
                    { 1, "Шоссе", 1 },
                    { 2, "Трейл", 1 },
                    { 3, "Шоссе", 2 },
                    { 4, "MTB", 2 },
                    { 5, "Классический стиль", 3 },
                    { 6, "Свободный стиль", 3 },
                    { 7, "Лыжероллеры", 3 },
                    { 8, "Бассейн", 4 },
                    { 9, "Открытая вода", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriberId",
                schema: "training",
                table: "Subscriptions",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubTypeId",
                schema: "training",
                table: "Subscriptions",
                column: "SubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TypeId",
                schema: "training",
                table: "Subscriptions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_AuthorId",
                schema: "training",
                table: "Trainings",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainingTypeId",
                schema: "training",
                table: "Trainings",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSubTypes_TrainingTypeId",
                schema: "training",
                table: "TrainingSubTypes",
                column: "TrainingTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "training");

            migrationBuilder.DropTable(
                name: "Trainings",
                schema: "training");

            migrationBuilder.DropTable(
                name: "TrainingSubTypes",
                schema: "training");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "training");

            migrationBuilder.DropTable(
                name: "TrainingTypes",
                schema: "training");
        }
    }
}
