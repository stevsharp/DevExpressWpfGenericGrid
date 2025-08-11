using Microsoft.EntityFrameworkCore;

namespace WpfAppGenericGrid.Model
{
    public class AppDbContext(string dbPath) : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={dbPath}");
    }
}
