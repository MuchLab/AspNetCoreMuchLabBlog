using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using MuchBlog.Infrastructure.Data.EntityConfiguration;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Infrastructure.Data
{
    public class BlogDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Essay> Essays { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<EssayClassification> EssayClassifications { get; set; }
        public DbSet<EssayImage> EssayImages { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //设置主键
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Essay>().HasKey(x => x.Id);
            modelBuilder.Entity<Classification>().HasKey(x => x.Id);
            modelBuilder.Entity<EssayImage>().HasKey(x => x.Id);
            //联合主键
            modelBuilder.Entity<EssayClassification>()
                .HasKey(x => new {x.ClassificationId, x.EssayId});

            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<Classification>().HasIndex(x => x.Name).IsUnique();

            //设置关系
            //User和Essay是一对多
            modelBuilder.Entity<Essay>()
                .HasOne(x => x.User)
                .WithMany(x => x.Essays)
                .HasForeignKey(x => x.UserId);
            //Essay和Classification是多对多
            modelBuilder.Entity<EssayClassification>()
                .HasOne(x => x.Essay)
                .WithMany(x => x.EssayClassifications)
                .HasForeignKey(x => x.EssayId);
            modelBuilder.Entity<EssayClassification>()
                .HasOne(x => x.Classification)
                .WithMany(x => x.EssayClassifications)
                .HasForeignKey(x => x.ClassificationId);
            //Essay和EssayImage是一对多
            modelBuilder.Entity<Essay>()
                .HasMany(x => x.EssayImages)
                .WithOne(x => x.Essay)
                .HasForeignKey(x => x.EssayId);
            modelBuilder.AddSeedData();
        }
    }
}