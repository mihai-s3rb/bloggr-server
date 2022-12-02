using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MinorChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Interests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interests_PostId",
                table: "Interests",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_Posts_PostId",
                table: "Interests",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Posts_PostId",
                table: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_Interests_PostId",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Interests");
        }
    }
}
