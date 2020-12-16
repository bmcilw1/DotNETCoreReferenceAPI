using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}