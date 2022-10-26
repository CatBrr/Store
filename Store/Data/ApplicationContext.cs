using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Models;
namespace Store.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
        public DbSet<kasutaja> kasutajad => Set<kasutaja>();
        public DbSet<master> teenindajad => Set<master>();
        public DbSet<teenust> teenused => Set<teenust>();
        public DbSet<keel> keelid => Set<keel>();
        public DbSet<loom> loomad => Set<loom>();
        public DbSet<klient> kliendit => Set<klient>();
        public ApplicationContext() { Database.EnsureDeleted(); Database.EnsureCreated(); }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbulder)
        {
            optionbulder.UseSqlite("Data Source=kasutajadBase.db");
        }

    }
}
