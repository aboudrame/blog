using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.Data.Migrations
{
    public partial class addcommenttoblogdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CSS",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "HTML",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "JavaScript",
                table: "Blogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CSS",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTML",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JavaScript",
                table: "Blogs",
                nullable: true);
        }
    }
}
