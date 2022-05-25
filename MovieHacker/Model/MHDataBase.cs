using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;
using System.Collections.Generic;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //simple primary key
            modelBuilder.Entity<Cinema>().HasKey(c => c.Id);
            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Session>().HasKey(s => s.Id);
            //composite primary key
            modelBuilder.Entity<FilmRoom>().HasKey(f => new {f.Id, f.Cinema});
        }
    }
}
