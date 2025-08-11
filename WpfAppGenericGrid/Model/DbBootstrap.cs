using Microsoft.EntityFrameworkCore;

namespace WpfAppGenericGrid.Model
{
    public static class DbBootstrap
    {
        public static async Task EnsureAndSeedAsync(AppDbContext db)
        {
            try
            {
                await db.Database.EnsureCreatedAsync();
                if (!await db.Set<Person>().AnyAsync())
                {
                    db.Set<Person>().AddRange(
                        new Person { FirstName = "John", LastName = "Doe", Age = 30 },
                        new Person { FirstName = "Jane", LastName = "Smith", Age = 25 },
                        new Person { FirstName = "Sam", LastName = "Wilson", Age = 35 }
                    );
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error during database initialization: {ex.Message}";
                throw;
            }
           
        }
    }
}
