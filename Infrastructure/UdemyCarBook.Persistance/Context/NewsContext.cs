using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Domain.Base;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Persistance.Context
{
    public class NewsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=Newspaper;User=root;Password=0G3hxwGD;",
                new MySqlServerVersion(new Version(8, 0, 40))
            )
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        public DbSet<BaseHistory> Histories { get; set; }
        public DbSet<About> Abouts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("char(36)")
                    .HasCharSet("ascii")
                    .HasCollation("ascii_general_ci");
            });
            modelBuilder.Entity<BaseHistory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("char(36)")
                    .HasCharSet("ascii")
                    .HasCollation("ascii_general_ci");

                entity.Property(e => e.EntityId)
                    .HasColumnType("char(36)")
                    .HasCharSet("ascii")
                    .HasCollation("ascii_general_ci");
            });
            modelBuilder.Entity<News>()
                .HasMany(n => n.Tags)
                .WithMany(t => t.News)
                .UsingEntity(j => j.ToTable("NewsTag"));

            modelBuilder.Entity<News>()
                .HasOne(n => n.CreatedByUser)
                .WithMany(u => u.CreatedNews)
                .HasForeignKey(n => n.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<News>()
                .HasOne(n => n.UpdatedByUser)
                .WithMany(u => u.UpdatedNews)
                .HasForeignKey(n => n.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.CreatedByUser)
                .WithMany(u => u.CreatedRoles)
                .HasForeignKey(r => r.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.UpdatedByUser)
                .WithMany(u => u.UpdatedRoles)
                .HasForeignKey(r => r.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.News)
                .WithOne(n => n.Author)
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.News)
                .WithOne(n => n.Category)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.News)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.NewsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SocialMedia>()
                .HasOne(s => s.Author)
                .WithMany(a => a.SocialMediaAccounts)
                .HasForeignKey(s => s.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .Ignore(u => u.CreatedRecords)
                .Ignore(u => u.UpdatedRecords);

            base.OnModelCreating(modelBuilder);
        }
    }
}
