using Microsoft.EntityFrameworkCore;
using BloggingApp.Domain.Entities;


namespace BloggingApp.Infrastructure.Data
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}

