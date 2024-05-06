using Microsoft.EntityFrameworkCore;

namespace PropertyAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Property> Properties => Set<Property>();
    }
}
