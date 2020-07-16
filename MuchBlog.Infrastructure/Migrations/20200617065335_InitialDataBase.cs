using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MuchBlog.Infrastructure.Migrations
{
    public partial class InitialDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 16, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTimeOffset>(nullable: false),
                    BirthPlace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Essays",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    LastModified = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Essays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Essays_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EssayClassifications",
                columns: table => new
                {
                    EssayId = table.Column<Guid>(nullable: false),
                    ClassificationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayClassifications", x => new { x.ClassificationId, x.EssayId });
                    table.ForeignKey(
                        name: "FK_EssayClassifications_Classifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EssayClassifications_Essays_EssayId",
                        column: x => x.EssayId,
                        principalTable: "Essays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EssayImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EssayId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EssayImages_Essays_EssayId",
                        column: x => x.EssayId,
                        principalTable: "Essays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EssayClassifications_EssayId",
                table: "EssayClassifications",
                column: "EssayId");

            migrationBuilder.CreateIndex(
                name: "IX_EssayImages_EssayId",
                table: "EssayImages",
                column: "EssayId");

            migrationBuilder.CreateIndex(
                name: "IX_Essays_UserId",
                table: "Essays",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EssayClassifications");

            migrationBuilder.DropTable(
                name: "EssayImages");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropTable(
                name: "Essays");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
