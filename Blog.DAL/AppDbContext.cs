using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
    }
}
