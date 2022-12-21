using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInterestUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterestUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestUsers", x => new { x.UserId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_InterestUsers_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InterestUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestUsers_InterestId",
                table: "InterestUsers",
                column: "InterestId");
        }
    }
}
