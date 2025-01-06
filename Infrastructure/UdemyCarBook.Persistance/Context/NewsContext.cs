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
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
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

            modelBuilder.Entity<Author>()
                .HasOne(a => a.CreatedByUser)
                .WithMany(u => u.CreatedAuthors)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Author>()
                .HasOne(a => a.UpdatedByUser)
                .WithMany(u => u.UpdatedAuthors)
                .HasForeignKey(a => a.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.News)
                .WithOne(n => n.Category)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedCategories)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany(u => u.UpdatedCategories)
                .HasForeignKey(c => c.UpdatedById)
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

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedComments)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.LastModifiedByUser)
                .WithMany(u => u.UpdatedComments)
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SocialMedia>()
                .HasOne(s => s.Author)
                .WithMany(a => a.SocialMediaAccounts)
                .HasForeignKey(s => s.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SocialMedia>()
                .HasOne(s => s.CreatedByUser)
                .WithMany(u => u.CreatedSocialMedias)
                .HasForeignKey(s => s.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SocialMedia>()
                .HasOne(s => s.UpdatedByUser)
                .WithMany(u => u.UpdatedSocialMedias)
                .HasForeignKey(s => s.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tag>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTags)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tag>()
                .HasOne(t => t.UpdatedByUser)
                .WithMany(u => u.UpdatedTags)
                .HasForeignKey(t => t.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedContacts)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.LastModifiedByUser)
                .WithMany(u => u.UpdatedContacts)
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Newsletter>()
                .HasOne(n => n.CreatedByUser)
                .WithMany(u => u.CreatedNewsletters)
                .HasForeignKey(n => n.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Newsletter>()
                .HasOne(n => n.UpdatedByUser)
                .WithMany(u => u.UpdatedNewsletters)
                .HasForeignKey(n => n.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Ignore(u => u.CreatedRecords)
                .Ignore(u => u.UpdatedRecords);

            base.OnModelCreating(modelBuilder);
        }
    }
}
