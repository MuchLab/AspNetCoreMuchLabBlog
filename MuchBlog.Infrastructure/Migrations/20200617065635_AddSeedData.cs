using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MuchBlog.Infrastructure.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Password", "UserName" },
                values: new object[] { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e4"), new DateTimeOffset(new DateTime(1992, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "US", "axlrose@xxx.com", "123456", "AxlRose" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Password", "UserName" },
                values: new object[] { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5"), new DateTimeOffset(new DateTime(1998, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "China", "zhangsan@xxx.com", "123456", "ZhangSan" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Password", "UserName" },
                values: new object[] { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e6"), new DateTimeOffset(new DateTime(1990, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "China", "lihua@xxx.com", "123456", "LiHua" });

            migrationBuilder.InsertData(
                table: "Essays",
                columns: new[] { "Id", "Content", "CreateDate", "LastModified", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e7"), "ABC", new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "AspNetCore——依赖注入", new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e4") },
                    { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e8"), "DEF", new DateTimeOffset(new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "AspNetCore——配置框架", new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e4") },
                    { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e9"), "GHI", new DateTimeOffset(new DateTime(2020, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "AspNetCore——日志框架", new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5") },
                    { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa110"), "123", new DateTimeOffset(new DateTime(2020, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "AspNetCore——AutoFac", new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5") },
                    { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa111"), "456", new DateTimeOffset(new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "AspNetCore——EFCore", new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e6") },
                    { new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa112"), "789", new DateTimeOffset(new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "AspNetCore——中间件", new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e6") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Essays",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa110"));

            migrationBuilder.DeleteData(
                table: "Essays",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa111"));

            migrationBuilder.DeleteData(
                table: "Essays",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa112"));

            migrationBuilder.DeleteData(
                table: "Essays",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e7"));

            migrationBuilder.DeleteData(
                table: "Essays",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e8"));

            migrationBuilder.DeleteData(
                table: "Essays",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e6"));
        }
    }
}
