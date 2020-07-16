using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Infrastructure.Data.EntityConfiguration
{
    public static class SeedDataExtension
    {
        public static void AddSeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                    UserName = "AxlRose",
                    BirthDate = new DateTimeOffset(new DateTime(1992, 8, 9)),
                    BirthPlace = "US",
                    Email = "axlrose@xxx.com",
                    Password = "123456"
                },
                new User
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"),
                    UserName = "ZhangSan",
                    BirthDate = new DateTimeOffset(new DateTime(1998, 8, 29)),
                    BirthPlace = "China",
                    Email = "zhangsan@xxx.com",
                    Password = "123456"
                },
                new User
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E6"),
                    UserName = "LiHua",
                    BirthDate = new DateTimeOffset(new DateTime(1990, 1, 19)),
                    BirthPlace = "China",
                    Email = "lihua@xxx.com",
                    Password = "123456"
                }
            });
            modelBuilder.Entity<Essay>().HasData(new List<Essay>
            {
                new Essay
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E7"),
                    Title = "AspNetCore——依赖注入",
                    Content = "ABC",
                    CreateDate = new DateTimeOffset(new DateTime(2020, 5, 4)),
                    LastModified = new DateTimeOffset(new DateTime(2020, 5, 4)),
                    UserId = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4")
                },
                new Essay
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E8"),
                    Title = "AspNetCore——配置框架",
                    Content = "DEF",
                    CreateDate = new DateTimeOffset(new DateTime(2020, 1, 14)),
                    LastModified = new DateTimeOffset(new DateTime(2020, 5, 4)),
                    UserId = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4")
                },
                new Essay
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E9"),
                    Title = "AspNetCore——日志框架",
                    Content = "GHI",
                    CreateDate = new DateTimeOffset(new DateTime(2020, 2, 24)),
                    LastModified = new DateTimeOffset(new DateTime(2020, 5, 4)),
                    UserId = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E5")
                },
                new Essay
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA110"),
                    Title = "AspNetCore——AutoFac",
                    Content = "123",
                    CreateDate = new DateTimeOffset(new DateTime(2020, 3, 14)),
                    LastModified = new DateTimeOffset(new DateTime(2020, 5, 4)),
                    UserId = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E5")
                },
                new Essay
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA111"),
                    Title = "AspNetCore——EFCore",
                    Content = "456",
                    CreateDate = new DateTimeOffset(new DateTime(2020, 1, 13)),
                    LastModified = new DateTimeOffset(new DateTime(2020, 5, 4)),
                    UserId = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E6")
                },
                new Essay
                {
                    Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA112"),
                    Title = "AspNetCore——中间件",
                    Content = "789",
                    CreateDate = new DateTimeOffset(new DateTime(2020, 6, 8)),
                    LastModified = new DateTimeOffset(new DateTime(2020, 5, 4)),
                    UserId = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E6")
                },
            });
        }
    }
}
