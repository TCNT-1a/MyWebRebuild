using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Web.Infra.Data;

namespace MyApp.Web.Infra.Data
{
    public class BloggingContext : DbContext
    {
        //"Data Source=Data/blogging.db",
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Data/blogging.db");
            }
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<HeadingTag> HeadingTags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
       
    }
}
/*
 * https://tuhocict.com/quan-he-1-nhieu-trong-entity-framework/#tao-quan-he-1-nhieu-tu-phia-nhieu
 * */