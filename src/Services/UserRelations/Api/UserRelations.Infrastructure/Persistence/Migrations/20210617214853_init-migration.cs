using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Kwetter.Services.UserRelations.Infrastucture.Persistence.Migrations
{
    public partial class initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Followings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FollowingUserId = table.Column<string>(type: "text", nullable: false),
                    FollowedUserId = table.Column<string>(type: "text", nullable: false),
                    FollowedSince = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followings", x => x.Id);
                    table.UniqueConstraint("AK_Followings_FollowedUserId_FollowingUserId", x => new { x.FollowedUserId, x.FollowingUserId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followings_FollowedUserId",
                table: "Followings",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_FollowingUserId",
                table: "Followings",
                column: "FollowingUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followings");
        }
    }
}
