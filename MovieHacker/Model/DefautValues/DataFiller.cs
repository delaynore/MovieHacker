using MovieHacker.Model.Tables;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MovieHacker.Model
{
    partial class DataFiller
    {
        public static void Fill()
        {
            using (MHDataBase db = new MHDataBase())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.SaveChanges();

                var genres = new[]
                {
                    new Genre { GenreName = "Комедия" },
                    new Genre { GenreName = "Мультфильм" },
                    new Genre { GenreName = "Триллер" },
                    new Genre { GenreName = "Ужасы" },
                    new Genre { GenreName = "Фантастика" },
                    new Genre { GenreName = "Боевик" },
                    new Genre { GenreName = "Драма"  },
                    new Genre { GenreName = "Документальный" },
                    new Genre { GenreName = "Биография" },
                    new Genre { GenreName = "Фэнтези" }
                };
                db.Genres.AddRange(genres);

                var cinemas = new[]
                {
                    new Cinema { CinemaName = "Петровский" },
                    new Cinema { CinemaName = "Матрица"},
                    new Cinema { CinemaName = "Киномакс"},
                    new Cinema { CinemaName = "Дядя Федор"},
                    new Cinema { CinemaName = "Россия"}
                };
                var rand = new Random();
                var filmrooms = new[] 
                {
                    new FilmRoom { NameRoom = "Красный", Capacity = rand.Next(20,50), Cinema = cinemas[0] },
                    new FilmRoom { NameRoom = "Желтый", Capacity = rand.Next(20,50), Cinema = cinemas[0] },
                    new FilmRoom { NameRoom = "Оранжевый", Capacity = rand.Next(20,50), Cinema = cinemas[0] },
                    new FilmRoom { NameRoom = "Васильковый", Capacity = rand.Next(20,50), Cinema = cinemas[0] },

                    new FilmRoom { NameRoom = "Первый", Capacity = rand.Next(30,60), Cinema = cinemas[1] },
                    new FilmRoom { NameRoom = "Второй", Capacity = rand.Next(30,60), Cinema = cinemas[1] },
                    new FilmRoom { NameRoom = "Третий", Capacity = rand.Next(30,60), Cinema = cinemas[1] },
                    new FilmRoom { NameRoom = "Четвертый", Capacity = rand.Next(20,50), Cinema = cinemas[1] },
                    new FilmRoom { NameRoom = "Пятый", Capacity = rand.Next(40,90), Cinema = cinemas[1] },
                    new FilmRoom { NameRoom = "Шестой", Capacity = rand.Next(10,20), Cinema = cinemas[1] },

                    new FilmRoom { NameRoom = "Кирпичный", Capacity = rand.Next(20,50), Cinema = cinemas[2] },
                    new FilmRoom { NameRoom = "Бетонный", Capacity = rand.Next(20,50), Cinema = cinemas[2] },

                    new FilmRoom { NameRoom = "Первый", Capacity = rand.Next(50,75), Cinema = cinemas[3] },
                    new FilmRoom { NameRoom = "Второй", Capacity = rand.Next(50,75), Cinema = cinemas[3] },
                    new FilmRoom { NameRoom = "Третий", Capacity = rand.Next(50,75), Cinema = cinemas[3] },
                    new FilmRoom { NameRoom = "Четвертый", Capacity = rand.Next(50,75), Cinema = cinemas[3] },

                    new FilmRoom { NameRoom = "Алмазный", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                    new FilmRoom { NameRoom = "Рубиновый", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                    new FilmRoom { NameRoom = "Аметистовый", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                    new FilmRoom { NameRoom = "Рубиновый", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                };

                cinemas[0].FilmRooms = new List<FilmRoom>(filmrooms[0..4]);
                cinemas[1].FilmRooms = new List<FilmRoom>(filmrooms[4..10]);
                cinemas[2].FilmRooms = new List<FilmRoom>(filmrooms[10..12]);
                cinemas[3].FilmRooms = new List<FilmRoom>(filmrooms[12..16]);
                cinemas[4].FilmRooms = new List<FilmRoom>(filmrooms[16..]);
                db.Cinemas.AddRange(cinemas);
                db.FilmRooms.AddRange(filmrooms);

                var movies = new[]
                {
                    new Movie { MovieName = "Люди в черном", DurationInMinutes = rand.Next(100, 240), Genre = genres[4]},
                    new Movie { MovieName = "Мстители", DurationInMinutes = rand.Next(100, 240), Genre = genres[4]},
                    new Movie { MovieName = "Человек-паук 3", DurationInMinutes = rand.Next(100, 240), Genre = genres[4]},
                    new Movie { MovieName = "Титаник", DurationInMinutes = rand.Next(100, 240), Genre = genres[6]},
                    new Movie { MovieName = "Маска", DurationInMinutes = rand.Next(100, 240), Genre = genres[0]},
                    new Movie { MovieName = "Волшебник страны Оз", DurationInMinutes = rand.Next(100, 240), Genre = genres[9]},
                };
                db.Movies.AddRange(movies);

                List<Session> sessions = new List<Session>();
                for (int i = 0; i < 100; i++)
                {
                    var num = rand.Next(20);
                    sessions.Add(new Session
                    {
                        Movie = movies[rand.Next(movies.Length)],
                        FilmRoom = filmrooms[num],
                        NumberAvailableSeats = filmrooms[num].Capacity,
                        Price = 320,
                        StartTime = DateTime.Parse($"{2022}-{rand.Next(1, 13)}-{rand.Next(1, 28)} {rand.Next(0,24)}:{rand.Next(0, 60)}:00", CultureInfo.InvariantCulture)
                    });
                }
                db.Sessions.AddRange(sessions);
                db.SaveChanges();
            }
        }
    }
}
