using Microsoft.EntityFrameworkCore;

namespace SelfHost.Infrastructure
{
    public class ReceivedValuesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        
        public ReceivedValuesDbContext(DbContextOptions<ReceivedValuesDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
            
        }
    }
}