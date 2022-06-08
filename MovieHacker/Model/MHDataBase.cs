using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model
{
    public class MHDataBase : DbContext
    {
        public DbSet<Cinema> Cinemas { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<FilmRoom> FilmRooms { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<MovieToGenre> MovieToGenres { get; set; } = null!;

        public MHDataBase()
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mh.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).HasMaxLength(50).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(2048).IsRequired(false);

                entity.HasMany(f => f.FilmRooms).WithOne(c => c.Cinema).HasForeignKey(c => c.CinemaId);

            });

            modelBuilder.Entity<FilmRoom>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.HasAlternateKey(f => new {f.Name, f.CinemaId});
                entity.Property(f=> f.Name).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).HasMaxLength(50).IsRequired();
                entity.HasAlternateKey(g => g.Name);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Title).HasMaxLength(100).IsRequired();
                entity.Property(m => m.Description).HasMaxLength(2048).IsRequired(false);
                entity.Property(m => m.Picture).IsRequired(false);
            });
            modelBuilder.Entity<MovieToGenre>(entity =>
            {
                entity.HasKey(x => new {x.MovieId, x.GenreId});
                entity.HasOne(x => x.Movie).WithMany(x => x.Genres).HasForeignKey(x => x.MovieId);
                entity.HasOne(x => x.Genre).WithMany(x => x.Movies).HasForeignKey(x => x.GenreId);
            });
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasOne(s => s.Movie).WithMany(m=>m.Sessions);
                entity.HasOne(s => s.FilmRoom).WithMany(f => f.Sessions);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
