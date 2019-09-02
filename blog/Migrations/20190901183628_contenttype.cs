using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blog.Migrations
{
    public partial class contenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentTypeId",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    ContentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.ContentTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ContentTypeId",
                table: "Blogs",
                column: "ContentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_ContentTypes_ContentTypeId",
                table: "Blogs",
                column: "ContentTypeId",
                principalTable: "ContentTypes",
                principalColumn: "ContentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_ContentTypes_ContentTypeId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ContentTypeId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ContentTypeId",
                table: "Blogs");
        }
    }
}
