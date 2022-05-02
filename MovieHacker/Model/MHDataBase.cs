using Microsoft.EntityFrameworkCore;

namespace MovieHacker.Model
{
    public class MHDataBase : DbContext
    {
        public MHDataBase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mh.db");
        }
    }
}
