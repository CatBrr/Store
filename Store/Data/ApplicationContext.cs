using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Models;
namespace Store.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<kasutaja> kasutajad => Set<kasutaja>();
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionbulder)
        {
            optionbulder.UseSqlite("Data Source=kasutajadBase.db");
        }
    }
}
