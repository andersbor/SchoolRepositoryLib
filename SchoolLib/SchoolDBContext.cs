using Microsoft.EntityFrameworkCore;
// NuGet package Microsoft.EntityFrameworkCore.SqlServer

namespace SchoolLib
{
    public class SchoolDBContext : DbContext
    {
        public SchoolDBContext(
            DbContextOptions<SchoolDBContext> options
            ) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        // more DBSets here
    }
}
