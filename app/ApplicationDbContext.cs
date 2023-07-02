using Microsoft.EntityFrameworkCore;

namespace sunstealer.mvc
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Models.Table1> table1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=AJMWIN11-01\SQLExpress;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Initial Catalog=database1;");
        }
    }
}
