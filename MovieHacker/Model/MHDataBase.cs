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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Genre>().HasData(new[]
        //    {
        //        new Genre { Id = 1, Name = "Комедия" },
        //        new Genre { Id = 2, Name = "Мультфильм" },
        //        new Genre { Id = 3, Name = "Триллер" },
        //        new Genre { Id = 4, Name = "Ужасы" },
        //        new Genre { Id = 5, Name = "Фантастика" },
        //        new Genre { Id = 6, Name = "Боевик" },
        //        new Genre { Id = 7, Name = "Драма"  },
        //        new Genre { Id = 8, Name = "Документальный" },
        //        new Genre { Id = 9, Name = "Биография" },
        //        new Genre { Id = 10, Name = "Фэнтези" }
        //    });

        //    modelBuilder.Entity<Cinema>().HasData(new[]
        //    {
        //        new Cinema { Id = 1, Name = "Петровский" },
        //        new Cinema { Id = 2, Name = "Матрица"},
        //        new Cinema { Id = 3, Name = "Киномакс"},
        //        new Cinema { Id = 4, Name = "Дядя Федор"},
        //        new Cinema { Id = 5, Name = "Россия"}
        //    });

        //}
    }
}
