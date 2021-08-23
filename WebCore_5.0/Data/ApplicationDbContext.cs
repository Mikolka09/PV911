using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebCore_5._0.Entities;
using WebCore_5._0.Entities.School;
using WebCore_5._0.Models;

namespace WebCore_5._0.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Groupp> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                    .HasMany(p => p.Tags)
                    .WithMany(t => t.Products)
                    .UsingEntity(j => j.ToTable("PivotProductTags"));

             //.UsingEntity<Dictionary<string, object>>(

             //      "PostTag",
             //      j => j
             //          .HasOne<Tag>()
             //          .WithMany()
             //          .HasForeignKey("TagId")
             //          .HasConstraintName("FK_PostTag_Tags_TagId")
             //          .OnDelete(DeleteBehavior.Cascade),
             //      j => j
             //          .HasOne<Post>()
             //          .WithMany()
             //          .HasForeignKey("PostId")
             //          .HasConstraintName("FK_PostTag_Posts_PostId")
             //          .OnDelete(DeleteBehavior.ClientCascade));

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
