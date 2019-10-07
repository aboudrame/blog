using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.Data.Migrations
{
    public partial class addTheCommentClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CSS",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTML",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JavaScript",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CSS",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "HTML",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "JavaScript",
                table: "Comments");
        }
    }
}
