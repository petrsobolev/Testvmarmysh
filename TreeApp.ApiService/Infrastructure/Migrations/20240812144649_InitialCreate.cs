using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TreeApp.ApiService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "trees");

            migrationBuilder.CreateTable(
                name: "ExceptionRecords",
                schema: "trees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: false),
                    QueryParametrs = table.Column<string>(type: "text", nullable: false),
                    BodyParametrs = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreeNodes",
                schema: "trees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    RootId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreeNodes_TreeNodes_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "trees",
                        principalTable: "TreeNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreeNodes_TreeNodes_RootId",
                        column: x => x.RootId,
                        principalSchema: "trees",
                        principalTable: "TreeNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionRecords_Timestamp",
                schema: "trees",
                table: "ExceptionRecords",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_TreeNodes_Name",
                schema: "trees",
                table: "TreeNodes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TreeNodes_ParentId",
                schema: "trees",
                table: "TreeNodes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TreeNodes_RootId",
                schema: "trees",
                table: "TreeNodes",
                column: "RootId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExceptionRecords",
                schema: "trees");

            migrationBuilder.DropTable(
                name: "TreeNodes",
                schema: "trees");
        }
    }
}
