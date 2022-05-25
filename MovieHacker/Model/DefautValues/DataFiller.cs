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
                    new Genre { Name = "Комедия" },
                    new Genre { Name = "Мультфильм" },
                    new Genre { Name = "Триллер" },
                    new Genre { Name = "Ужасы" },
                    new Genre { Name = "Фантастика" },
                    new Genre { Name = "Боевик" },
                    new Genre { Name = "Драма"  },
                    new Genre { Name = "Документальный" },
                    new Genre { Name = "Биография" },
                    new Genre { Name = "Фэнтези" }
                };
                db.Genres.AddRange(genres);

                var cinemas = new[]
                {
                    new Cinema { Name = "Петровский" },
                    new Cinema { Name = "Матрица"},
                    new Cinema { Name = "Киномакс"},
                    new Cinema { Name = "Дядя Федор"},
                    new Cinema { Name = "Россия"}
                };
                var rand = new Random();
                var filmrooms = new[] 
                {
                    new FilmRoom { Name = "Красный", Capacity = rand.Next(20,50), Cinema = cinemas[0] },
                    new FilmRoom { Name = "Желтый", Capacity = rand.Next(20,50), Cinema = cinemas[0] },
                    new FilmRoom { Name = "Оранжевый", Capacity = rand.Next(20,50), Cinema = cinemas[0] },
                    new FilmRoom { Name = "Васильковый", Capacity = rand.Next(20,50), Cinema = cinemas[0] },

                    new FilmRoom { Name = "Первый", Capacity = rand.Next(30,60), Cinema = cinemas[1] },
                    new FilmRoom { Name = "Второй", Capacity = rand.Next(30,60), Cinema = cinemas[1] },
                    new FilmRoom { Name = "Третий", Capacity = rand.Next(30,60), Cinema = cinemas[1] },
                    new FilmRoom { Name = "Четвертый", Capacity = rand.Next(20,50), Cinema = cinemas[1] },
                    new FilmRoom { Name = "Пятый", Capacity = rand.Next(40,90), Cinema = cinemas[1] },
                    new FilmRoom { Name = "Шестой", Capacity = rand.Next(10,20), Cinema = cinemas[1] },

                    new FilmRoom { Name = "Кирпичный", Capacity = rand.Next(20,50), Cinema = cinemas[2] },
                    new FilmRoom { Name = "Бетонный", Capacity = rand.Next(20,50), Cinema = cinemas[2] },

                    new FilmRoom { Name = "Первый", Capacity = rand.Next(50,75), Cinema = cinemas[3] },
                    new FilmRoom { Name = "Второй", Capacity = rand.Next(50,75), Cinema = cinemas[3] },
                    new FilmRoom { Name = "Третий", Capacity = rand.Next(50,75), Cinema = cinemas[3] },
                    new FilmRoom { Name = "Четвертый", Capacity = rand.Next(50,75), Cinema = cinemas[3] },

                    new FilmRoom { Name = "Алмазный", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                    new FilmRoom { Name = "Рубиновый", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                    new FilmRoom { Name = "Аметистовый", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                    new FilmRoom { Name = "Рубиновый", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
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
                    new Movie { Title = "Люди в черном", DurationInMinutes = rand.Next(100, 240), Genre = genres[4]},
                    new Movie { Title = "Мстители", DurationInMinutes = rand.Next(100, 240), Genre = genres[4]},
                    new Movie { Title = "Человек-паук 3", DurationInMinutes = rand.Next(100, 240), Genre = genres[4]},
                    new Movie { Title = "Титаник", DurationInMinutes = rand.Next(100, 240), Genre = genres[6]},
                    new Movie { Title = "Маска", DurationInMinutes = rand.Next(100, 240), Genre = genres[0]},
                    new Movie { Title = "Волшебник страны Оз", DurationInMinutes = rand.Next(100, 240), Genre = genres[9]},
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
