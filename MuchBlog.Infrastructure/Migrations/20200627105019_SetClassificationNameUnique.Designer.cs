﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MuchBlog.Infrastructure.Data;

namespace MuchBlog.Infrastructure.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20200627105019_SetClassificationNameUnique")]
    partial class SetClassificationNameUnique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.Classification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Classifications");
                });

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.Essay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Essays");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e7"),
                            Content = "ABC",
                            CreateDate = new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            LastModified = new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Title = "AspNetCore——依赖注入",
                            UserId = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e4")
                        },
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e8"),
                            Content = "DEF",
                            CreateDate = new DateTimeOffset(new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            LastModified = new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Title = "AspNetCore——配置框架",
                            UserId = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e4")
                        },
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e9"),
                            Content = "GHI",
                            CreateDate = new DateTimeOffset(new DateTime(2020, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            LastModified = new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Title = "AspNetCore——日志框架",
                            UserId = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5")
                        },
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa110"),
                            Content = "123",
                            CreateDate = new DateTimeOffset(new DateTime(2020, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            LastModified = new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Title = "AspNetCore——AutoFac",
                            UserId = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5")
                        },
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa111"),
                            Content = "456",
                            CreateDate = new DateTimeOffset(new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            LastModified = new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Title = "AspNetCore——EFCore",
                            UserId = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e6")
                        },
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa112"),
                            Content = "789",
                            CreateDate = new DateTimeOffset(new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            LastModified = new DateTimeOffset(new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Title = "AspNetCore——中间件",
                            UserId = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e6")
                        });
                });

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.EssayClassification", b =>
                {
                    b.Property<Guid>("ClassificationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EssayId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClassificationId", "EssayId");

                    b.HasIndex("EssayId");

                    b.ToTable("EssayClassifications");
                });

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.EssayImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EssayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EssayId");

                    b.ToTable("EssayImages");
                });

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("BirthPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e4"),
                            BirthDate = new DateTimeOffset(new DateTime(1992, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "US",
                            Email = "axlrose@xxx.com",
                            Password = "123456",
                            UserName = "AxlRose"
                        },
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e5"),
                            BirthDate = new DateTimeOffset(new DateTime(1998, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "China",
                            Email = "zhangsan@xxx.com",
                            Password = "123456",
                            UserName = "ZhangSan"
                        },
                        new
                        {
                            Id = new Guid("f9168c5e-ceb2-4faa-b6bf-329bf39fa1e6"),
                            BirthDate = new DateTimeOffset(new DateTime(1990, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "China",
                            Email = "lihua@xxx.com",
                            Password = "123456",
                            UserName = "LiHua"
                        });
                });

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.Essay", b =>
                {
                    b.HasOne("MuchBlog.Infrastructure.Entities.User", "User")
                        .WithMany("Essays")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.EssayClassification", b =>
                {
                    b.HasOne("MuchBlog.Infrastructure.Entities.Classification", "Classification")
                        .WithMany("EssayClassifications")
                        .HasForeignKey("ClassificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MuchBlog.Infrastructure.Entities.Essay", "Essay")
                        .WithMany("EssayClassifications")
                        .HasForeignKey("EssayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MuchBlog.Infrastructure.Entities.EssayImage", b =>
                {
                    b.HasOne("MuchBlog.Infrastructure.Entities.Essay", "Essay")
                        .WithMany("EssayImages")
                        .HasForeignKey("EssayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
