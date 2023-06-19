using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class Adding_creatorId_to_Url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urls_Users_UserId",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Urls",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Urls_UserId",
                table: "Urls",
                newName: "IX_Urls_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_Users_CreatorId",
                table: "Urls",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urls_Users_CreatorId",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Urls",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Urls_CreatorId",
                table: "Urls",
                newName: "IX_Urls_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_Users_UserId",
                table: "Urls",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
