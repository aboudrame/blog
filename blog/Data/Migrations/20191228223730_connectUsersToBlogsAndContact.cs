using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.Data.Migrations
{
    public partial class connectUsersToBlogsAndContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Blogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId",
                table: "Contacts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_UserID",
                table: "Blogs",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_UserId",
                table: "Contacts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_UserID",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_UserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Blogs");
        }
    }
}
