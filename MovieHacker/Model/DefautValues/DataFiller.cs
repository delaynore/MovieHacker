using MovieHacker.Model.Tables;
using MovieHacker.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Drawing;

namespace MovieHacker.Model
{
    partial class DataFiller
    {
        public static void Fill()
        {
            using (MHDataBase db = new())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var genres = new[]
                {
                    new Genre { Name = "Комедия" },//0
                    new Genre { Name = "Мультфильм" },//1
                    new Genre { Name = "Триллер" },//2
                    new Genre { Name = "Ужасы" },//3
                    new Genre { Name = "Фантастика" },//4
                    new Genre { Name = "Боевик" },//5
                    new Genre { Name = "Драма"  },//6
                    new Genre { Name = "Документальный" },//7
                    new Genre { Name = "Биография" },//8
                    new Genre { Name = "Фэнтези" }//9
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
                db.Cinemas.AddRange(cinemas);
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
                    new FilmRoom { Name = "Бриллиантовый", Capacity = rand.Next(60,90), Cinema = cinemas[4] },
                };
                
                db.FilmRooms.AddRange(filmrooms);
                var movies = new[]
                {
                    new Movie { Title = "Люди в черном", DurationInMinutes = rand.Next(100, 240), IsActual = true, Picture = ImageBase64Converter.ImageToBase64(Resource.ManInBlack)},//0
                    new Movie { Title = "Мстители", DurationInMinutes = rand.Next(100, 240), IsActual = true, Picture = ImageBase64Converter.ImageToBase64(Resource.Avengers)},//1
                    new Movie { Title = "Человек-паук 3", DurationInMinutes = rand.Next(100, 240), IsActual = true, Picture = ImageBase64Converter.ImageToBase64(Resource.SpiderMan3)},//2
                    new Movie { Title = "Титаник", DurationInMinutes = rand.Next(100, 240), IsActual = true, Picture = ImageBase64Converter.ImageToBase64(Resource.Titanic)},//3
                    new Movie { Title = "Маска", DurationInMinutes = rand.Next(100, 240), IsActual = true, Picture = ImageBase64Converter.ImageToBase64(Resource.TheMask)},//4
                    new Movie { Title = "Волшебник страны Оз", DurationInMinutes = rand.Next(100, 240), IsActual = true, Picture = ImageBase64Converter.ImageToBase64(Resource.TheWizardOfOz)},//5
                };
                db.Movies.AddRange(movies);
                var movietogenre = new[]
                {
                    new MovieToGenre { Genre = genres[4], Movie = movies[0]},
                    new MovieToGenre { Genre = genres[4], Movie = movies[5]},
                    new MovieToGenre { Genre = genres[4], Movie = movies[2]},
                    new MovieToGenre { Genre = genres[4], Movie = movies[4]},
                    new MovieToGenre { Genre = genres[4], Movie = movies[1]},
                    new MovieToGenre { Genre = genres[6], Movie = movies[3]},
                    new MovieToGenre { Genre = genres[9], Movie = movies[5]},
                    new MovieToGenre { Genre = genres[5], Movie = movies[0]},
                    new MovieToGenre { Genre = genres[5], Movie = movies[1]},
                    new MovieToGenre { Genre = genres[5], Movie = movies[2]},
                };
                db.MovieToGenres.AddRange(movietogenre);
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
