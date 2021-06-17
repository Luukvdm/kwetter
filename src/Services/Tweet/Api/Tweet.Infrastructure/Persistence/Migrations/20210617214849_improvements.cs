using Microsoft.EntityFrameworkCore.Migrations;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence.Migrations
{
    public partial class improvements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tags",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TweetMessages_CreatorId",
                table: "TweetMessages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TweetMessages_CreatorId",
                table: "TweetMessages");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tags");
        }
    }
}
