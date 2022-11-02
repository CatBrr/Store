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
        public DbSet<master> teenindajad => Set<master>();
        public DbSet<keel> keelid => Set<keel>();
        public DbSet<loom> loomad => Set<loom>();
        public DbSet<sugu> sugud => Set<sugu>();
        public DbSet<viilatuup> viilatuupid => Set<viilatuup>();
        public DbSet<klient> kliendit => Set<klient>();
        public DbSet<teenust> teenused => Set<teenust>();
        public DbSet<iseloomu> iseloomud => Set<iseloomu>();
        public DbSet<bron> bronid => Set<bron>();
        public ApplicationContext() {  Database.EnsureCreated(); }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbulder)
        {
            optionbulder.UseSqlite("Data Source=kasutajadBase.db");
        }

    }
}
