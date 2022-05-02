using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model
{
    public class MHDataBase : DbContext
    {
        public DbSet<Cinema> Cinemas => Set<Cinema>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<FilmRoom> FilmRooms => Set<FilmRoom>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Session> Sessions => Set<Session>();

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
