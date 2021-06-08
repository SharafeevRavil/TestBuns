using Buns.Domain.Entities.Buns;
using Microsoft.EntityFrameworkCore;

namespace Buns.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BunType> BunTypes { get; set; }
        public DbSet<Bun> Buns { get; set; }
        public DbSet<PretzelBun> PretzelBuns { get; set; }
        public DbSet<SourCreamBun> SourCreamBuns { get; set; }
    }
}