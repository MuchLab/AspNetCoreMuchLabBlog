using Microsoft.EntityFrameworkCore.Migrations;

namespace MuchBlog.Infrastructure.Migrations
{
    public partial class SetClassificationNameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Classifications_Name",
                table: "Classifications",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classifications_Name",
                table: "Classifications");
        }
    }
}
