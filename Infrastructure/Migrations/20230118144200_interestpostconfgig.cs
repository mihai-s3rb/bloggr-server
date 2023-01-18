using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class interestpostconfgig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestPosts_Interests_InterestId",
                table: "InterestPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestPosts_Posts_PostId",
                table: "InterestPosts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ec6cfd79-56d2-4ae6-9b37-4eae57db9dcd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7125c2bc-658d-468d-a5b2-b1363393ee18");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPosts_Interests_InterestId",
                table: "InterestPosts",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPosts_Posts_PostId",
                table: "InterestPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestPosts_Interests_InterestId",
                table: "InterestPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestPosts_Posts_PostId",
                table: "InterestPosts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ca294c4f-8420-4872-8336-709220631565");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "12845fc9-2338-47c4-9873-e7895880b58d");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPosts_Interests_InterestId",
                table: "InterestPosts",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPosts_Posts_PostId",
                table: "InterestPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
