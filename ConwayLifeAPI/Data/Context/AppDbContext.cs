using ConwayLifeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConwayLifeAPI.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; } = null;
    }
}
