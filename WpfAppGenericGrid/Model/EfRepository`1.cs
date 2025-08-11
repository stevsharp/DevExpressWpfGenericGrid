using Microsoft.EntityFrameworkCore;

namespace WpfAppGenericGrid.Model
{
    public class EfRepository<T>(AppDbContext db) where T : class
    {
        public Task<List<T>> GetAllAsync(CancellationToken ct = default)
            => db.Set<T>().AsNoTracking().ToListAsync(ct);
    }
}